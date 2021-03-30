﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BonusBot.GuildSettingsModule.Language {
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
    internal class ModuleTexts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ModuleTexts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BonusBot.GuildSettingsModule.Language.ModuleTexts", typeof(ModuleTexts).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Every module has its own settings.
        ///To show every setting of a module you can use this command:
        ///&quot;!config help [module]&quot;
        ///
        ///With this command you can set a value:
        ///&quot;!config [module] [setting] [value]&quot;
        ///
        ///This command returns the value of a setting:
        ///&quot;!config get [module] [setting]&quot;
        ///
        ///Loaded modules:
        ///{0}.
        /// </summary>
        internal static string HelpTextMain {
            get {
                return ResourceManager.GetString("HelpTextMain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All settings of this module {0}:
        ///{1}.
        /// </summary>
        internal static string HelpTextModule {
            get {
                return ResourceManager.GetString("HelpTextModule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the boolean &apos;{0}&apos;..
        /// </summary>
        internal static string SettingBoolSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingBoolSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the channel &apos;{0}&apos;..
        /// </summary>
        internal static string SettingChannelSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingChannelSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the emote &apos;{0}&apos;..
        /// </summary>
        internal static string SettingEmoteSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingEmoteSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value for the setting &apos;{0}&apos; for the module &apos;{1}&apos; is:
        ///{2}.
        /// </summary>
        internal static string SettingGetInfo {
            get {
                return ResourceManager.GetString("SettingGetInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; in module &apos;{1}&apos; does not exist..
        /// </summary>
        internal static string SettingInThisModuleDoesNotExist {
            get {
                return ResourceManager.GetString("SettingInThisModuleDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the message with this content:
        ///{0}.
        /// </summary>
        internal static string SettingMessageSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingMessageSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the number &apos;{0}&apos;..
        /// </summary>
        internal static string SettingNumberSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingNumberSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the role &apos;{0}&apos;..
        /// </summary>
        internal static string SettingRoleSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingRoleSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the string &apos;{0}&apos;..
        /// </summary>
        internal static string SettingStringSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingStringSavedSuccessfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting was successfully saved with the user &apos;{0}&apos;..
        /// </summary>
        internal static string SettingUserSavedSuccessfully {
            get {
                return ResourceManager.GetString("SettingUserSavedSuccessfully", resourceCulture);
            }
        }
    }
}
