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
        ///   Looks up a localized string similar to Input could not be parsed as a boolean..
        /// </summary>
        public static string CommandInvalidBooleanError {
            get {
                return ResourceManager.GetString("CommandInvalidBooleanError", resourceCulture);
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
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; for module &apos;{1}&apos; contains the id of a channel ({2}) which does not exist (anymore). Please use &apos;!setting {1} {0} [VALUE]&apos; to set a correct channel id..
        /// </summary>
        public static string SettingChannelDoesNotExist {
            get {
                return ResourceManager.GetString("SettingChannelDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; is invalid. Please use &apos;!setting {1} {0} [VALUE]&apos; to set it again..
        /// </summary>
        public static string SettingInvalidError {
            get {
                return ResourceManager.GetString("SettingInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The setting &apos;{0}&apos; is missing. Please use &apos;!setting {1} {0} [VALUE]&apos; to set it..
        /// </summary>
        public static string SettingMissingError {
            get {
                return ResourceManager.GetString("SettingMissingError", resourceCulture);
            }
        }
    }
}
