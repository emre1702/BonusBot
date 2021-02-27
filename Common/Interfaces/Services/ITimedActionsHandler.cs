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

        IEnumerable<TimedActions> Get(string unban, string moduleName);

        IEnumerable<TimedActions> Get(string actionType, Assembly module);

        IEnumerable<TimedActions> Get(string actionType, Type module);

        Task Save();
    }
}