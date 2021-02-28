﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BonusBot.Common.Languages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BonusBot.Common.Languages.Texts", typeof(Texts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; error occured. Message:
        ///&quot;{1}&quot;
        ///
        ///Used command: 
        ///&apos;{2}&apos;.
        /// </summary>
        public static string CommandExecutedError {
            get {
                return ResourceManager.GetString("CommandExecutedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input could not be parsed as a boolean..
        /// </summary>
        public static string CommandInvalidBooleanError {
            get {
                return ResourceManager.GetString("CommandInvalidBooleanError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input is not in Hex or RGB format and could not be parsed..
        /// </summary>
        public static string CommandInvalidColorError {
            get {
                return ResourceManager.GetString("CommandInvalidColorError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input &apos;{0}&apos; is not in valid date and/or time format..
        /// </summary>
        public static string CommandInvalidDateTimeOffsetError {
            get {
                return ResourceManager.GetString("CommandInvalidDateTimeOffsetError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input could not be parsed as a time span..
        /// </summary>
        public static string CommandInvalidTimeSpanError {
            get {
                return ResourceManager.GetString("CommandInvalidTimeSpanError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input could not be parsed as a user..
        /// </summary>
        public static string CommandInvalidUserError {
            get {
                return ResourceManager.GetString("CommandInvalidUserError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This command is only allowed in a guild channel..
        /// </summary>
        public static string CommandOnlyAllowedInGuild {
            get {
                return ResourceManager.GetString("CommandOnlyAllowedInGuild", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The guild is not initialized yet. Try again after some seconds..
        /// </summary>
        public static string GuildNotInitializedYet {
            get {
                return ResourceManager.GetString("GuildNotInitializedYet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;TimeZone&apos; setting (&apos;{0}&apos;) in &apos;Common&apos; module is invalid.
        ///
        ///If you are an admin of the guild, please use &apos;!setting Common TimeZone [time zone]&apos; to set a valid time zone (e.g. &apos;UTC&apos; or &apos;CET&apos;).
        ///If not, tell an admin to do it..
        /// </summary>
        public static string InvalidTimeZoneIdError {
            get {
                return ResourceManager.GetString("InvalidTimeZoneIdError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This module (&apos;{0}&apos;) has been disabled in this guild.
        ///If you are an administrator, you can enable it again with this command:
        ///&quot;!module + {0}&quot;.
        /// </summary>
        public static string ModuleIsDisabledError {
            get {
                return ResourceManager.GetString("ModuleIsDisabledError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value for {0} has to be between {1} and {2}..
        /// </summary>
        public static string NumberRangeError {
            get {
                return ResourceManager.GetString("NumberRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role &apos;{0}&apos; is required for this action..
        /// </summary>
        public static string RoleRequiredError {
            get {
                return ResourceManager.GetString("RoleRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; for module &apos;{1}&apos; contains a channel which does not exist (anymore). 
        ///
        ///If you are an admin of the guild, please use &apos;!setting {1} {0} [VALUE]&apos; to set a correct channel.
        ///If not, tell an admin to do it..
        /// </summary>
        public static string SettingChannelDoesNotExist {
            get {
                return ResourceManager.GetString("SettingChannelDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; for module &apos;{1}&apos; contains an emote which does not exist (anymore). 
        ///
        ///If you are an admin of the guild, please use &apos;!setting {1} {0} [VALUE]&apos; to set a correct emote.
        ///If not, tell an admin to do it..
        /// </summary>
        public static string SettingEmoteDoesNotExist {
            get {
                return ResourceManager.GetString("SettingEmoteDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; is invalid. 
        ///
        ///If you are an admin of the guild, please use &apos;!setting {1} {0} [VALUE]&apos; to set it again.
        ///If not, tell an admin to do it..
        /// </summary>
        public static string SettingInvalidError {
            get {
                return ResourceManager.GetString("SettingInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; is missing.
        ///
        ///If you are an admin of the guild, please use &apos;!setting {1} {0} [VALUE]&apos; to set it.
        ///If not, tell an admin to do it..
        /// </summary>
        public static string SettingMissingError {
            get {
                return ResourceManager.GetString("SettingMissingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The target &apos;{0}&apos; is higher in the hierachie than you..
        /// </summary>
        public static string TargetIsHigherInHierarchyError {
            get {
                return ResourceManager.GetString("TargetIsHigherInHierarchyError", resourceCulture);
            }
        }
    }
}
