using System.Text.Json;
using System.Text.Json.Serialization;

namespace RSApiClient.JsonConverters
{
    public class PriceJsonConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    return reader.GetInt32().ToString();
                case JsonTokenType.String:
                    return reader.GetString();
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
