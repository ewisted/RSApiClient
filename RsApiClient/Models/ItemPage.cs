using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class ItemPage
    {
        [JsonPropertyName("total")]
        public int TotalForCategory { get; set; }

        [JsonIgnore]
        public int Current { get; set; }

        [JsonIgnore]
        public int Offset { get; set; }

        [JsonIgnore]
        public int Page { get; set; }

        [JsonIgnore]
        public char Character { get; set; }

        [JsonIgnore]
        public ItemCategory Category { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<Item> Items { get; set; }

        public ItemPage()
        {
            TotalForCategory = 0;
            Current = 0;
            Offset = 0;
            Page = 0;
            Character = 'a';
            Category = ItemCategory.Ammo;
            Items = new List<Item>();
        }
    }
}
