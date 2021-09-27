using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class ItemResult
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }

        public ItemResult()
        {
            Item = new Item();
        }
    }
}
