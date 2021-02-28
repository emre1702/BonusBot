﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BonusBot.AudioModule.Language {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BonusBot.AudioModule.Language.ModuleTexts", typeof(ModuleTexts).Assembly);
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
        ///   Looks up a localized string similar to Gain value must be between -0.25 - 1.0 and Band value must be between 0 - 14..
        /// </summary>
        internal static string EqualizerParamsError {
            get {
                return ResourceManager.GetString("EqualizerParamsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Finished playing: {0}. 
        ///There are no more items left in the queue..
        /// </summary>
        internal static string FinishedPlayingNoMoreItemsInQueueInfo {
            get {
                return ResourceManager.GetString("FinishedPlayingNoMoreItemsInQueueInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Finished playing: {0}
        ///Now playing: {1}.
        /// </summary>
        internal static string FinishedPlayingNowPlayingInfo {
            get {
                return ResourceManager.GetString("FinishedPlayingNowPlayingInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Found {0} tracks..
        /// </summary>
        internal static string FoundTracksAmountInfo {
            get {
                return ResourceManager.GetString("FoundTracksAmountInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current volume: {0}..
        /// </summary>
        internal static string GetVolumeInfo {
            get {
                return ResourceManager.GetString("GetVolumeInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The last search result was empty. There are no songs to play..
        /// </summary>
        internal static string LastSearchEmptyError {
            get {
                return ResourceManager.GetString("LastSearchEmptyError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Load failed. The url could be wrong or there could be an internal problem..
        /// </summary>
        internal static string LavaLinkLoadError {
            get {
                return ResourceManager.GetString("LavaLinkLoadError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nothing found..
        /// </summary>
        internal static string LavaLinkNoMatchesError {
            get {
                return ResourceManager.GetString("LavaLinkNoMatchesError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lyrics for: {0}.
        /// </summary>
        internal static string LyricsForInfo {
            get {
                return ResourceManager.GetString("LyricsForInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lyrics could not be found for &apos;{0}&apos;..
        /// </summary>
        internal static string LyricsNotFoundError {
            get {
                return ResourceManager.GetString("LyricsNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Start and stop must be atleast 0..
        /// </summary>
        internal static string NegativeStartOrStopError {
            get {
                return ResourceManager.GetString("NegativeStartOrStopError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You haven&apos;t use the search command yet. Search for a song first..
        /// </summary>
        internal static string NoLastSearchError {
            get {
                return ResourceManager.GetString("NoLastSearchError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to I&apos;m currently not connected to a voice channel or maybe can&apos;t play a song at this time..
        /// </summary>
        internal static string NoPlayerForGuildError {
            get {
                return ResourceManager.GetString("NoPlayerForGuildError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You&apos;re not connected to a voice channel..
        /// </summary>
        internal static string NotConnectToVoiceChannel {
            get {
                return ResourceManager.GetString("NotConnectToVoiceChannel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This operation is invalid since player isn&apos;t actually playing anything..
        /// </summary>
        internal static string NothingPlayingError {
            get {
                return ResourceManager.GetString("NothingPlayingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This module has not been initialized yet..
        /// </summary>
        internal static string NotInitializedYetError {
            get {
                return ResourceManager.GetString("NotInitializedYetError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Currently there is no track running..
        /// </summary>
        internal static string NoTrackRunningError {
            get {
                return ResourceManager.GetString("NoTrackRunningError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is no track to replay..
        /// </summary>
        internal static string NoTrackToReplayError {
            get {
                return ResourceManager.GetString("NoTrackToReplayError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No volume has been saved for this guild yet..
        /// </summary>
        internal static string NoVolumeSavedError {
            get {
                return ResourceManager.GetString("NoVolumeSavedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Now playing: {0}.
        /// </summary>
        internal static string NowPlayingInfo {
            get {
                return ResourceManager.GetString("NowPlayingInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This command is only allowed in a guild text channel..
        /// </summary>
        internal static string OnlyAllowedInGuildChat {
            get {
                return ResourceManager.GetString("OnlyAllowedInGuildChat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The player is paused, use the resume command to continue..
        /// </summary>
        internal static string PlayerIsPausedInfo {
            get {
                return ResourceManager.GetString("PlayerIsPausedInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The position ({0} - {1}%) is actually larger than the actual length..
        /// </summary>
        internal static string PositionLargeThanLengthError {
            get {
                return ResourceManager.GetString("PositionLargeThanLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are no tracks in the queue..
        /// </summary>
        internal static string QueueEmptyInfo {
            get {
                return ResourceManager.GetString("QueueEmptyInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Replaying song: {0}..
        /// </summary>
        internal static string ReplayingSongInfo {
            get {
                return ResourceManager.GetString("ReplayingSongInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The position {0} is greater than current track&apos;s length of {1}..
        /// </summary>
        internal static string SeekPositionTooHigh {
            get {
                return ResourceManager.GetString("SeekPositionTooHigh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Set position to {0} ({1}%)..
        /// </summary>
        internal static string SetPositionInfo {
            get {
                return ResourceManager.GetString("SetPositionInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid input.
        ///Please use ?% for percent, ?s for seconds, ?m for minutes or minutes:seconds for a specific time..
        /// </summary>
        internal static string SetPositionWrongFormatError {
            get {
                return ResourceManager.GetString("SetPositionWrongFormatError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Changed volume to {0}..
        /// </summary>
        internal static string SetVolumeInfo {
            get {
                return ResourceManager.GetString("SetVolumeInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The queue is now shuffled..
        /// </summary>
        internal static string ShuffleInfo {
            get {
                return ResourceManager.GetString("ShuffleInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skipped: {0}
        ///Now playing: {1}.
        /// </summary>
        internal static string SkippedTrackNowPlayingInfo {
            get {
                return ResourceManager.GetString("SkippedTrackNowPlayingInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Skipped: {0}
        ///There are no more tracks left in the queue..
        /// </summary>
        internal static string SkippedTrackNowStoppedInfo {
            get {
                return ResourceManager.GetString("SkippedTrackNowStoppedInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A song at this search number does not exist..
        /// </summary>
        internal static string SongAtThisSearchNumberNotExistsError {
            get {
                return ResourceManager.GetString("SongAtThisSearchNumberNotExistsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stop time must be greater than start time..
        /// </summary>
        internal static string StartTimeLessThanStopTimeError {
            get {
                return ResourceManager.GetString("StartTimeLessThanStopTimeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} has been put into the playlist..
        /// </summary>
        internal static string TrackHasBeenEnqueuedInfo {
            get {
                return ResourceManager.GetString("TrackHasBeenEnqueuedInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track {0}, requested by {1}, has been removed from the queue..
        /// </summary>
        internal static string TrackHasBeenRemovedFromQueueInfo {
            get {
                return ResourceManager.GetString("TrackHasBeenRemovedFromQueueInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track {0} got stuck for {1} ms and therefore skipped.
        ///Now playing: {2}.
        /// </summary>
        internal static string TrackStuckNextSongInfo {
            get {
                return ResourceManager.GetString("TrackStuckNextSongInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track {0} got stuck for {1} ms and therefore skipped.
        ///There are no more items left in the queue..
        /// </summary>
        internal static string TrackStuckNoMoreItemsInQueueInfo {
            get {
                return ResourceManager.GetString("TrackStuckNoMoreItemsInQueueInfo", resourceCulture);
            }
        }
    }
}
