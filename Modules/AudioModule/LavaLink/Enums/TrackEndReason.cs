using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TrackEndReason
    {
        [EnumMember(Value = "FINISHED")]
        Finished,

        [EnumMember(Value = "LOAD_FAILED")]
        LoadFailed,

        [EnumMember(Value = "STOPPED")]
        Stopped,

        [EnumMember(Value = "REPLACED")]
        Replaced,

        [EnumMember(Value = "CLEANUP")]
        Cleanup
    }
}