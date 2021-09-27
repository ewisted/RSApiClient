using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public class Item
    {
        [JsonPropertyName("icon")]
        public string IconUrl { get; set; }

        [JsonPropertyName("icon_large")]
        public string IconLargeUrl { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("typeIcon")]
        public string TypeIconUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("members")]
        [JsonConverter(typeof(IsMembersJsonConverter))]
        public bool IsMembers { get; set; }

        [JsonPropertyName("current")]
        public ItemPriceTrend CurrentTrend { get; set; }

        [JsonPropertyName("today")]
        public ItemPriceTrend DayTrend { get; set; }

        [JsonPropertyName("day30")]
        public ItemChangeTrend MonthTrend { get; set; }

        [JsonPropertyName("day90")]
        public ItemChangeTrend ThreeMonthTrend { get; set; }

        [JsonPropertyName("day180")]
        public ItemChangeTrend SixMonthTrend { get; set; }

        public Item()
        {
            IconUrl = "";
            IconLargeUrl = "";
            Id = -1;
            Type = "";
            TypeIconUrl = "";
            Name = "";
            Description = "";
            IsMembers = false;
            CurrentTrend = new ItemPriceTrend();
            DayTrend = new ItemPriceTrend();
            MonthTrend = new ItemChangeTrend();
            ThreeMonthTrend = new ItemChangeTrend();
            SixMonthTrend = new ItemChangeTrend();
        }
    }
}
