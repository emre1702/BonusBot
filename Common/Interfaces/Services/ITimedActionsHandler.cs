using BonusBot.Database.Entities.Cases;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Services
{
    public interface ITimedActionsHandler
    {
        void AddToCache(IEnumerable<TimedActions> newActions);

        void Remove(TimedActions action);

        List<TimedActions> Get(string unban, string moduleName);

        List<TimedActions> Get(string actionType, Assembly module);

        List<TimedActions> Get(string actionType, Type module);

        Task Save();
    }
}