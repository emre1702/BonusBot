using Discord;
using System;
using System.Globalization;

namespace BonusBot.Common.Defaults
{
    public static class Constants
    {
        public static CultureInfo DefaultCultureInfo => new("en-US");
        public static string TokenEnvironmentVariable => "BONUSBOT_TOKEN";
        public static string DefaultCommandPrefix => "!";
        public static string DefaultBotName => "BonusBot";
        public static bool DefaultCommandMentionAllowed => true;
        public static string ModuleDeactivatedDbKey => "ModuleDeactivated";
        public static LogSeverity ConsoleHelperLogLevel => LogSeverity.Info;
        public static bool IsDocker => Environment.GetEnvironmentVariable("ISDOCKER") == "true";
        public static string Activity => "www.bonusbot.net";
    }
}