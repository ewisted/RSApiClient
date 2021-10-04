using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RSApiClient.GrandExchange.Models
{
    public record CategoryItemCatalogue
    {
        [JsonPropertyName("alpha")]
        public IEnumerable<CharacterItemCount> CharacterCounts { get; set; }

        [JsonConstructor]
        public CategoryItemCatalogue(IEnumerable<CharacterItemCount> characterCounts) => CharacterCounts = characterCounts;
    }

    public record CharacterItemCount
    {
        [JsonPropertyName("letter")]
        public char Letter { get; set; }

        [JsonPropertyName("items")]
        public int Count { get; set; }

        [JsonConstructor]
        public CharacterItemCount(char letter, int count) => (Letter, Count) = (letter, count);
    }
}
