﻿using System;
using Color = System.Drawing.Color;
using static Colorful.Console;
using Discord;
using BonusBot.Common.Enums;

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

            Append(logo, Color.Orchid);
            Append($"{Environment.NewLine}   {new string('=', 100)}", Color.AliceBlue);
            Write(Environment.NewLine);
        }

        private static void HandleLog(LogSeverity severity, LogSource source, string message, Exception? exception)
        {
            var (color, simplified) = ProcessLogSeverity(severity);
            Append($"    {simplified}", color);

            (color, simplified) = ProcessSource(source);
            Append($" -> {simplified} -> ", color);

            if (!string.IsNullOrWhiteSpace(message))
                Append(message, Color.White);

            if (exception is { })
                Append(exception.Message, Color.IndianRed);

            Write(Environment.NewLine);
        }

        private static void Append(string message, Color color)
        {
            ForegroundColor = color;
            Write(message);
        }

        private static (Color Color, string Simplified) ProcessSource(LogSource source)
            => source switch
            {
                LogSource.Discord
                    => (Color.RoyalBlue, "DSCD"),
                LogSource.Core
                    => (Color.DarkSalmon, "CORE"),
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