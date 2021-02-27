using BonusBot.Database.Entities.Cases;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Services
{
    public interface ITimedActionsHandler
    {
        Task Add(TimedActions newAction);

        Task Remove(TimedActions action);

        List<TimedActions> Get(string actionType, string moduleName);

        List<TimedActions> Get(string actionType, Assembly module);

        List<TimedActions> Get(string actionType, Type module);

        TimedActions? Get(ulong targetId, string actionType, string moduleName);

        TimedActions? Get(ulong targetId, string actionType, Assembly module);

        TimedActions? Get(ulong targetId, string actionType, Type module);

        Task Save();

        Task DoWithTransaction(Func<ValueTask> action, Func<ValueTask>? actionFinally);
    }
}