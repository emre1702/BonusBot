﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BonusBot.DateTimeTestModule.Language {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BonusBot.DateTimeTestModule.Language.ModuleTexts", typeof(ModuleTexts).Assembly);
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
        ///   Looks up a localized string similar to Time: {0}
        ///Local time: {1}
        ///Utc time: {2}
        ///Guild timezone: {3}
        ///Timezone: {4}
        ///Daylight saving: {5}
        ///Utc offset: {6}.
        /// </summary>
        internal static string OutputLocalTimeInfo {
            get {
                return ResourceManager.GetString("OutputLocalTimeInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parsed:
        ///Time: {0}
        ///Local time: {1}
        ///Local date time: {2}
        ///Utc time {3}
        ///Utc date time {4}
        ///Daylight saving: {5}
        ///Offset {6}.
        /// </summary>
        internal static string OutputParsedDateTimeOffsetInfo {
            get {
                return ResourceManager.GetString("OutputParsedDateTimeOffsetInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to From database:
        ///Time: {0}
        ///Local time: {1}
        ///Utc time {2}
        ///Daylight saving: {3}.
        /// </summary>
        internal static string OutputParsedDbDateTimeOffsetInfo {
            get {
                return ResourceManager.GetString("OutputParsedDbDateTimeOffsetInfo", resourceCulture);
            }
        }
    }
}
