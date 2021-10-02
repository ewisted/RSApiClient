using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public abstract record ItemTrendBase
    {
        [JsonPropertyName("trend")]
        [JsonConverter(typeof(TrendTypeJsonConverter))]
        public TrendType Trend { get; init; }
    }

    public enum TrendType
    {
        None = 0,
        Negative = 1,
        Neutral = 2,
        Positive = 3
    }
}
