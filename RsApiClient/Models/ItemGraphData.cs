using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class ItemGraphData
    {
        [JsonPropertyName("daily")]
        [JsonConverter(typeof(GraphDataJsonConverter))]
        public Dictionary<DateTimeOffset, int> Daily { get; set; }

        [JsonPropertyName("average")]
        [JsonConverter(typeof(GraphDataJsonConverter))]
        public Dictionary<DateTimeOffset, int> Average { get; set; }

        public ItemGraphData()
        {
            Daily = new Dictionary<DateTimeOffset, int>();
            Average = new Dictionary<DateTimeOffset, int>();
        }
    }
}
