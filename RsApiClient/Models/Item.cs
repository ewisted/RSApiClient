using RSApiClient.JsonConverters;
using System.Text.Json.Serialization;

namespace RSApiClient.Models
{
    public record Item
    {
        [JsonPropertyName("icon")]
        public string IconUrl { get; init; }

        [JsonPropertyName("icon_large")]
        public string IconLargeUrl { get; init; }

        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("type")]
        public string Type { get; init; }

        [JsonPropertyName("typeIcon")]
        public string TypeIconUrl { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("members")]
        [JsonConverter(typeof(IsMembersJsonConverter))]
        public bool IsMembers { get; init; }

        [JsonPropertyName("current")]
        public ItemPriceTrend CurrentTrend { get; init; }

        [JsonPropertyName("today")]
        public ItemPriceTrend DayTrend { get; init; }

        [JsonPropertyName("day30")]
        public ItemChangeTrend MonthTrend { get; init; }

        [JsonPropertyName("day90")]
        public ItemChangeTrend ThreeMonthTrend { get; init; }

        [JsonPropertyName("day180")]
        public ItemChangeTrend SixMonthTrend { get; init; }

        [JsonConstructor]
        public Item(
            string iconUrl,
            string iconLargeUrl,
            int id,
            string type,
            string typeIconUrl,
            string name,
            string description,
            bool isMembers,
            ItemPriceTrend currentTrend,
            ItemPriceTrend dayTrend,
            ItemChangeTrend monthTrend,
            ItemChangeTrend threeMonthTrend,
            ItemChangeTrend sixMonthTrend) =>
            (IconUrl, IconLargeUrl, Id, Type, TypeIconUrl, Name, Description, IsMembers, CurrentTrend, DayTrend, MonthTrend, ThreeMonthTrend, SixMonthTrend) =
            (iconUrl, iconLargeUrl, id, type, typeIconUrl, name, description, isMembers, currentTrend, dayTrend, monthTrend, threeMonthTrend, sixMonthTrend);
    }
}
