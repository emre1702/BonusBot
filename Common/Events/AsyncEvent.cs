using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusBot.Helper.Events
{
    public class AsyncEvent<T>
    {
        protected readonly List<Func<T, Task>> _invocationList = new();

        protected AsyncEvent()
        {
        }

        public static AsyncEvent<T>? operator -(
            AsyncEvent<T>? e, Func<T, Task> callback)
        {
            if (callback == null) throw new NullReferenceException("callback is null");
            if (e == null) return null;

            lock (e._invocationList)
                e._invocationList.Remove(callback);

            return e;
        }

        public static AsyncEvent<T> operator +(
                    AsyncEvent<T>? e, Func<T, Task> callback)
        {
            if (e == null) e = new AsyncEvent<T>();

            lock (e._invocationList)
                e._invocationList.Add(callback);

            return e;
        }

        public virtual async Task InvokeAsync(T arg)
        {
            List<Func<T, Task>> tmpInvocationList;
            lock (_invocationList)
                tmpInvocationList = new List<Func<T, Task>>(_invocationList);

            foreach (var callback in tmpInvocationList)
                await callback(arg).ConfigureAwait(false);
        }

        public void Clear()
        {
            lock (_invocationList)
                _invocationList.Clear();
        }
    }
}