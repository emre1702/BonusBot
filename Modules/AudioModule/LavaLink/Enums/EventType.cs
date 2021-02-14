using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Enums
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    internal enum EventType
    {
        [EnumMember(Value = "TrackStartEvent")]
        TrackStart,

        [EnumMember(Value = "TrackEndEvent")]
        TrackEnd,

        [EnumMember(Value = "TrackStuckEvent")]
        TrackStuck,

        [EnumMember(Value = "TrackExceptionEvent")]
        TrackException,

        [EnumMember(Value = "WebSocketClosedEvent")]
        WebSocketClosed
    }
}