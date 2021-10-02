using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public record ItemChangeTrend : ItemTrendBase
    {
        [JsonPropertyName("change")]
        [JsonConverter(typeof(PriceJsonConverter))]
        public int PercentageChanged { get; init; }

        [JsonConstructor]
        public ItemChangeTrend(TrendType trend, int percentageChanged) => (Trend, PercentageChanged) = (trend, percentageChanged);
    }
}
