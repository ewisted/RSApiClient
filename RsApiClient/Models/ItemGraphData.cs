using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public record ItemGraphData
    {
        [JsonPropertyName("daily")]
        [JsonConverter(typeof(GraphDataJsonConverter))]
        public Dictionary<DateTimeOffset, int> Daily { get; init; }

        [JsonPropertyName("average")]
        [JsonConverter(typeof(GraphDataJsonConverter))]
        public Dictionary<DateTimeOffset, int> Average { get; init; }

        [JsonConstructor]
        public ItemGraphData(Dictionary<DateTimeOffset, int> daily, Dictionary<DateTimeOffset, int> average) => (Daily, Average) = (daily, average);
    }
}
