using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum TrackEndReason
    {
        [EnumMember(Value = "FINISHED")]
        Finished,

        [EnumMember(Value = "LOAD_FAILED")]
        Load_Failed,

        [EnumMember(Value = "STOPPED")]
        Stopped,

        [EnumMember(Value = "REPLACED")]
        Replaced,

        [EnumMember(Value = "CLEANUP")]
        Cleanup
    }
}