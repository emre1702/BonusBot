using BonusBot.AudioModule.LavaLink;
using Discord;
using System;
using System.Threading.Tasks;

namespace BonusBot.AudioModule.Extensions
{
    internal static class FuncExtensions
    {
        internal static void WriteLog(this Func<LogMessage, Task> log, LogSeverity severity, string message, Exception? exception = null)
        {
            if (severity > Configuration.InternalSeverity)
                return;

            var logMessage = new LogMessage(severity, nameof(AudioModule), message, exception);
            log?.Invoke(logMessage);
        }
    }
}