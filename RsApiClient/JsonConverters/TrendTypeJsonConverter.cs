using RSApiClient.GrandExchange.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RSApiClient.JsonConverters
{
    public class TrendTypeJsonConverter : JsonConverter<TrendType>
    {
        public override TrendType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "negative":
                    return TrendType.Negative;
                case "neutral":
                    return TrendType.Neutral;
                case "positive":
                    return TrendType.Positive;
                default:
                    return TrendType.None;
            }
        }

        public override void Write(Utf8JsonWriter writer, TrendType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLower());
        }
    }
}
