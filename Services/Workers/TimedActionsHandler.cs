using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Services.Workers
{
    internal class TimedActionsHandler : ITimedActionsHandler
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

        private readonly List<TimedActions> _loadedTimedActions;
        private readonly BonusDbContext _dbContext;

        public TimedActionsHandler(BonusDbContextFactory dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            RemoveExpiredActionsSync();
            _loadedTimedActions = _dbContext.TimedActions.ToList();
        }

        private void RemoveExpiredActionsSync()
        {
            var currentTime = DateTime.UtcNow;
            var expiredActions = _dbContext.TimedActions.AsQueryable().Where(a => a.MaxDelay.HasValue && a.AtDateTime + a.MaxDelay < currentTime).ToList();
            if (expiredActions.Count == 0) return;
            _dbContext.TimedActions.RemoveRange(expiredActions);
            _dbContext.SaveChanges();
        }

        public Task Add(TimedActions newAction)
        {
            return DoWithSemaphore(() =>
            {
                lock (_loadedTimedActions)
                    _loadedTimedActions.Add(newAction);

                _dbContext.TimedActions.Add(newAction);
                return Task.CompletedTask;
            });
        }

        public Task Remove(TimedActions action)
        {
            return DoWithSemaphore(() =>
            {
                lock (_loadedTimedActions)
                    _loadedTimedActions.Remove(action);

                _dbContext.TimedActions.Remove(action);
                return Task.CompletedTask;
            });
        }

        public List<TimedActions> Get(string actionType, string moduleName)
        {
            moduleName = moduleName.ToModuleName();
            lock (_loadedTimedActions)
            {
                RemoveExpiredNoLock();
                return _loadedTimedActions.Where(a => a.ActionType == actionType && a.Module == moduleName && a.AtDateTime <= DateTime.UtcNow).ToList();
            }
        }

        public List<TimedActions> Get(string actionType, Assembly module)
            => Get(actionType, module.ToModuleName());

        public List<TimedActions> Get(string actionType, Type module)
            => Get(actionType, module.GetModuleName());

        public TimedActions? Get(ulong targetId, string actionType, string moduleName)
        {
            moduleName = moduleName.ToModuleName();
            lock (_loadedTimedActions)
            {
                RemoveExpiredNoLock();
                return _loadedTimedActions.Where(a => a.ActionType == actionType && a.Module == moduleName && a.TargetId == targetId && a.AtDateTime <= DateTime.UtcNow).FirstOrDefault();
            }
        }

        public TimedActions? Get(ulong targetId, string actionType, Assembly module)
            => Get(targetId, actionType, module.ToModuleName());

        public TimedActions? Get(ulong targetId, string actionType, Type module)
            => Get(targetId, actionType, module.GetModuleName());

        private void RemoveExpiredNoLock()
            => _loadedTimedActions.RemoveAll(a => a.MaxDelay is { } && a.AtDateTime + a.MaxDelay < DateTime.UtcNow);

        public Task Save()
            => _dbContext.SaveChangesAsync();

        public async Task DoWithTransaction(Func<ValueTask> action, Func<ValueTask>? actionFinally)
        {
            await DoWithSemaphore(async () =>
            {
                IDbContextTransaction? transaction = null;
                try
                {
                    transaction = await _dbContext.Database.BeginTransactionAsync();
                    await action();

                    await transaction.CommitAsync();
                }
                catch
                {
                    if (transaction is { })
                        await transaction.RollbackAsync();
                    throw;
                }
                finally
                {
                    if (actionFinally is { })
                        await actionFinally.Invoke();
                }
            });
        }

        private async Task DoWithSemaphore(Func<Task> action)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}