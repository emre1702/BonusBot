using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum LoadType
    {
        [EnumMember(Value = "TRACK_LOADED")]
        TrackLoaded,

        [EnumMember(Value = "PLAYLIST_LOADED")]
        PlaylistLoaded,

        [EnumMember(Value = "SEARCH_RESULT")]
        SearchResult,

        [EnumMember(Value = "NO_MATCHES")]
        NoMatches,

        [EnumMember(Value = "LOAD_FAILED")]
        LoadFailed
    }
}