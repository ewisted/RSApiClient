using System.Text.Json;
using System.Text.Json.Serialization;

namespace RSApiClient.JsonConverters
{
    public class GraphDataJsonConverter : JsonConverter<Dictionary<DateTimeOffset, int>>
    {
        public override Dictionary<DateTimeOffset, int>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected a start object token but got {reader.TokenType}");
            }

            var dictionary = new Dictionary<DateTimeOffset, int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                // Get the key
                string propertyName = reader.GetString() ?? throw new JsonException();
                if (!long.TryParse(propertyName, out long epochTime))
                {
                    throw new JsonException($"Unable to convert \"{propertyName}\" to DateTimeOffset");
                }
                DateTimeOffset key = DateTimeOffset.FromUnixTimeMilliseconds(epochTime);

                // Get the value
                int value = JsonSerializer.Deserialize<int>(ref reader, options);

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<DateTimeOffset, int> dictionary, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach ((DateTimeOffset key, int value) in dictionary)
            {
                string propertyName = key.ToUnixTimeMilliseconds().ToString();
                writer.WritePropertyName(propertyName);

                JsonSerializer.Serialize(writer, value, options);
            }

            writer.WriteEndObject();
        }
    }
}
