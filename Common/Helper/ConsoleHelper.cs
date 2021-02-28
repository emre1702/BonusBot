using BonusBot.Common.Defaults;
using BonusBot.Common.Enums;
using Discord;
using System;
using static Colorful.Console;
using Color = System.Drawing.Color;

namespace BonusBot.Common.Helper
{
    public static class ConsoleHelper
    {
        private static readonly object _lockObj = new object();

        public static void Log(LogSeverity severity, LogSource source, string message, Exception? exception = null)
        {
            lock (_lockObj)
            {
                HandleLog(severity, source, message, exception);
            }
        }

        public static void Log(LogSeverity severity, string source, string message, Exception? exception = null)
        {
            if (!Enum.TryParse<LogSource>(source, out var sourceEnum))
                sourceEnum = LogSource.Unknown;
            Log(severity, sourceEnum, message, exception);
        }

        public static void Log(LogMessage message)
        {
            var source = message.Source;
            if (!Enum.TryParse<LogSource>(source, out var sourceEnum))
                sourceEnum = LogSource.Unknown;
            Log(message.Severity, sourceEnum, message.Message, message.Exception);
        }

        public static void PrintHeader()
        {
            var logo =
                @"
 /$$$$$$$                                          /$$$$$$$              /$$
| $$__  $$                                        | $$__  $$            | $$
| $$  \ $$  /$$$$$$  /$$$$$$$  /$$   /$$  /$$$$$$$| $$  \ $$  /$$$$$$  /$$$$$$
| $$$$$$$  /$$__  $$| $$__  $$| $$  | $$ /$$_____/| $$$$$$$  /$$__  $$|_  $$_/
| $$__  $$| $$  \ $$| $$  \ $$| $$  | $$|  $$$$$$ | $$__  $$| $$  \ $$  | $$
| $$  \ $$| $$  | $$| $$  | $$| $$  | $$ \____  $$| $$  \ $$| $$  | $$  | $$ /$$
| $$$$$$$/|  $$$$$$/| $$  | $$|  $$$$$$/ /$$$$$$$/| $$$$$$$/|  $$$$$$/  |  $$$$/
|_______/  \______/ |__/  |__/ \______/ |_______/ |_______/  \______/    \___/";

            AppendLine(logo, Color.Orchid);
            WriteLine(Environment.NewLine);
        }

        private static void HandleLog(LogSeverity severity, LogSource source, string message, Exception? exception)
        {
            if ((int)severity > (int)Constants.ConsoleHelperLogLevel)
                return;
            var (color, simplified) = ProcessLogSeverity(severity);
            Append($"    {simplified}", color);

            (color, simplified) = ProcessSource(source);
            Append($" -> {simplified} -> ", color);

            AppendLine(message ?? string.Empty, Color.White);

            if (exception is { })
                AppendLine(exception.ToString(), Color.IndianRed);
        }

        private static void Append(string message, Color color)
        {
            ForegroundColor = color;
            Write(message);
        }

        private static void AppendLine(string message, Color color)
        {
            ForegroundColor = color;
            WriteLine(message);
        }

        private static (Color Color, string Simplified) ProcessSource(LogSource source)
            => source switch
            {
                LogSource.Discord
                    => (Color.RoyalBlue, "DSCD"),
                LogSource.Core
                    => (Color.DarkSalmon, "CORE"),
                LogSource.Module
                    => (Color.DarkGreen, "MDUL"),
                LogSource.AudioModule
                    => (Color.DarkGreen, "AMDL"),
                LogSource.Job
                    => (Color.Aquamarine, "JOB"),
                _ => (Color.Gray, "UKWN")
            };

        private static (Color Color, string Simplified) ProcessLogSeverity(LogSeverity logSeverity)
            => logSeverity switch
            {
                LogSeverity.Info
                    => (Color.Green, "INFO"),
                LogSeverity.Debug
                    => (Color.SandyBrown, "DBUG"),
                LogSeverity.Error
                    => (Color.Maroon, "EROR"),
                LogSeverity.Verbose
                    => (Color.SandyBrown, "VROS"),
                LogSeverity.Warning
                    => (Color.Yellow, "WARN"),
                LogSeverity.Critical
                    => (Color.Maroon, "CRIT"),
                _ => (Color.Gray, "UKWN")
            };
    }
}