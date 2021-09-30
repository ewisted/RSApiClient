using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class ItemChangeTrend : ItemTrendBase
    {
        [JsonPropertyName("change")]
        [JsonConverter(typeof(PriceJsonConverter))]
        public int PercentageChanged { get; set; }

        public ItemChangeTrend()
        {
            PercentageChanged = 0;
        }
    }
}
