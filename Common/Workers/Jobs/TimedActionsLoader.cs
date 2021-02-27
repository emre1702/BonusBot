using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Workers.Jobs
{
    public class TimedActionsLoader : JobBase
    {
        protected override TimeSpan DelayTime => TimeSpan.FromSeconds(5);

        private DateTime _lastLoadTime = new(1990, 1, 1);
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

        private readonly BonusDbContext _bonusDbContext;
        private readonly ITimedActionsHandler _timedActionsHandler;

        public TimedActionsLoader(BonusDbContextFactory bonusDbContextFactory, ITimedActionsHandler timedActionsHandler)
            => (_bonusDbContext, _timedActionsHandler) = (bonusDbContextFactory.CreateDbContext(), timedActionsHandler);

        protected override async ValueTask DoWork()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                var newActions = await LoadNewAction();
                if (newActions.Count == 0) return;
                _timedActionsHandler.AddToCache(newActions);

                _lastLoadTime = DateTime.UtcNow;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private Task<List<TimedActions>> LoadNewAction()
        {
            var currentDate = DateTime.UtcNow;
            return _bonusDbContext.TimedActions.AsQueryable().Where(a => a.AddedDateTime >= _lastLoadTime && a.AtDateTime <= _lastLoadTime).ToListAsync();
        }
    }
}