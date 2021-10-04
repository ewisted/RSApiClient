using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.GrandExchange.Models
{
    public record ItemPriceTrend : ItemTrendBase
    {
        [JsonPropertyName("price")]
        [JsonConverter(typeof(PriceJsonConverter))]
        public int Price { get; init; }

        [JsonConstructor]
        public ItemPriceTrend(TrendType trend, int price) => (Trend, Price) = (trend, price);
    }
}
