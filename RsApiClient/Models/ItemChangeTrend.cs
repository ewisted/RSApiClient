using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class ItemChangeTrend : ItemTrendBase
    {
        [JsonPropertyName("change")]
        public string PercentageChanged { get; set; }

        public ItemChangeTrend()
        {
            PercentageChanged = "";
        }
    }
}
