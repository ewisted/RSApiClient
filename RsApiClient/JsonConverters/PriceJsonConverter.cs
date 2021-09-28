using System.Text.Json;
using System.Text.Json.Serialization;

namespace RSApiClient.JsonConverters
{
    public class PriceJsonConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    return reader.GetInt32();
                case JsonTokenType.String:
                    return GetIntValueForString(reader.GetString());
                default:
                    throw new JsonException($"Invalid token when attempting to deserialize price: {reader.TokenType}");
            }
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }

        private int GetIntValueForString(string? value)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value.Contains("%"))
                {
                    value = string.Concat(value.Skip(1).TakeWhile(c => c != '.'));
                    result = int.Parse(value);
                }
                else
                {
                    value = value.Replace("-", "").Replace("+", "").Trim();
                    if (value.Contains('m', StringComparison.InvariantCultureIgnoreCase))
                    {
                        value = value.Replace("m", "");
                        var doubleValue = double.Parse(value);
                        result = Convert.ToInt32(doubleValue * 1000000);
                    }
                    else if (value.Contains('k', StringComparison.InvariantCultureIgnoreCase))
                    {
                        value = value.Replace("k", "");
                        var doubleValue = double.Parse(value);
                        result = Convert.ToInt32(doubleValue * 1000);
                    }
                    else
                    {
                        value = value.Replace(",", "");
                        result = int.Parse(value);
                    }
                }
            }

            return result;
        }
    }
}
