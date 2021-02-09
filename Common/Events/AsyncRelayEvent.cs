using BonusBot.Helper.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.Common.Events
{
    public class AsyncRelayEvent<T> : AsyncEvent<T>
    {
        private readonly List<T> _invokeArgsHistory = new();

        protected AsyncRelayEvent() : base()
        {
        }

        public static AsyncRelayEvent<T>? operator -(
            AsyncRelayEvent<T>? e, Func<T, Task> callback)
        {
            if (callback == null) throw new NullReferenceException("callback is null");
            if (e == null) return null;

            lock (e._invocationList)
                e._invocationList.Remove(callback);

            return e;
        }

        public static AsyncRelayEvent<T> operator +(
                    AsyncRelayEvent<T>? e, Func<T, Task> callback)
        {
            if (e == null) e = new AsyncRelayEvent<T>();

            lock (e._invocationList)
                e._invocationList.Add(callback);

            lock (e._invokeArgsHistory)
                foreach (var arg in e._invokeArgsHistory)
                    callback.Invoke(arg);

            return e;
        }

        public override Task InvokeAsync(T arg)
        {
            lock (_invokeArgsHistory) _invokeArgsHistory.Add(arg);
            return base.InvokeAsync(arg);
        }
    }
}