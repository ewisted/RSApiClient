using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record RS3HiscoresLiteSnapshot
    {
        public Skill Summoning { get; init; }
        public Skill Dungeoneering { get; init; }
        public Skill Divination { get; init; }
        public Skill Invention { get; init; }
        public Skill Archaeology { get; init; }
    }
}
