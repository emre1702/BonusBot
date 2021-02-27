using BonusBot.Common.Enums;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Workers;
using Discord;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BonusBot.Common.Workers
{
    public abstract class JobBase : IJob
    {
        protected Task? _runningTask;
        protected CancellationTokenSource _cancellationTokenSource = new();

        public virtual void Start()
        {
            _runningTask = Run();
            ConsoleHelper.Log(LogSeverity.Info, LogSource.Job, $"Job '{GetType().Name}' started.");
        }

        protected virtual async Task Run()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    await DoWork();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.Log(LogSeverity.Error, LogSource.Job, $"Error in job '{GetType().Name}'", ex);
                }
                await Task.Delay(DelayTime);
            }
        }

        protected abstract ValueTask DoWork();

        protected abstract TimeSpan DelayTime { get; }
    }
}