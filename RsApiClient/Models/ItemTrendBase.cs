using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public abstract class ItemTrendBase
    {
        [JsonPropertyName("trend")]
        [JsonConverter(typeof(TrendTypeJsonConverter))]
        public TrendType Trend { get; set; }

        public ItemTrendBase()
        {
            Trend = TrendType.None;
        }
    }

    public enum TrendType
    {
        None = 0,
        Negative = 1,
        Neutral = 2,
        Positive = 3
    }
}
