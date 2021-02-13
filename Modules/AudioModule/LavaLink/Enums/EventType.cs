using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BonusBot.AudioModule.LavaLink.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    internal enum EventType
    {
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