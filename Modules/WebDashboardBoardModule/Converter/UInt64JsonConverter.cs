using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BonusBot.WebDashboardBoardModule.Converter
{
    public class UInt64JsonConverter : JsonConverter<ulong>
    {
        public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType == JsonTokenType.Number ? reader.GetUInt64() : ulong.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value);
    }
}
