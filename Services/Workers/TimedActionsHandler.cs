using BonusBot.Common.Extensions;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Database;
using BonusBot.Database.Entities.Cases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Services.Workers
{
    internal class TimedActionsHandler : ITimedActionsHandler
    {
        private readonly List<TimedActions> _loadedTimedActions = new();
        private readonly BonusDbContext _dbContext;

        public TimedActionsHandler(BonusDbContextFactory dbContextFactory)
            => _dbContext = dbContextFactory.CreateDbContext();

        public void AddToCache(IEnumerable<TimedActions> newActions)
        {
            lock (_loadedTimedActions)
                _loadedTimedActions.AddRange(newActions);
        }

        public void Remove(TimedActions action)
        {
            lock (_loadedTimedActions)
                _loadedTimedActions.Remove(action);

            _dbContext.TimedActions.Remove(action);
        }

        public List<TimedActions> Get(string actionType, string moduleName)
        {
            moduleName = moduleName.ToModuleName();
            lock (_loadedTimedActions)
            {
                return _loadedTimedActions.Where(a => a.ActionType == actionType && a.Module == moduleName).ToList();
            }
        }

        public List<TimedActions> Get(string actionType, Assembly module)
            => Get(actionType, module.ToModuleName());

        public List<TimedActions> Get(string actionType, Type module)
            => Get(actionType, module.GetModuleName());

        public Task Save()
            => _dbContext.SaveChangesAsync();
    }
}