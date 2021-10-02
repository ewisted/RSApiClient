using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public record ItemResult
    {
        [JsonPropertyName("item")]
        public Item Item { get; init; }

        public ItemResult(Item item) => Item = item;
    }
}
