using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class ItemPriceTrend : ItemTrendBase
    {
        [JsonPropertyName("price")]
        [JsonConverter(typeof(PriceJsonConverter))]
        public int Price { get; set; }

        public ItemPriceTrend()
        {
            Price = 0;
        }
    }
}
