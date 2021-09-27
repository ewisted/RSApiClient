using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSApiClient.JsonConverters
{
    public class GraphDataJsonConverter : JsonConverter<Dictionary<DateTimeOffset, int>>
    {
        public override Dictionary<DateTimeOffset, int>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var dictionary = new Dictionary<DateTimeOffset, int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                // Get the key
                string propertyName = reader.GetString() ?? throw new JsonException();
                if (!long.TryParse(propertyName, out long epochTime))
                {
                    throw new JsonException($"Unable to convert \"{propertyName}\" to DateTimeOffset.");
                }
                DateTimeOffset key = DateTimeOffset.FromUnixTimeMilliseconds(epochTime);

                // Get the value
                int value = JsonSerializer.Deserialize<int>(ref reader, options);

                dictionary.Add(key, value);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<DateTimeOffset, int> dictionary, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach ((DateTimeOffset key, int value) in dictionary)
            {
                string propertyName = key.ToUnixTimeSeconds().ToString();
                writer.WritePropertyName(propertyName);

                JsonSerializer.Serialize(writer, value, options);
            }

            writer.WriteEndObject();
        }
    }
}
