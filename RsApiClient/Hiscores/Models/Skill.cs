using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record Skill : RankingSnapshot
    {
        public int Level { get; init; }
        public int Experience { get; init; }
		public Skill(string name, int rank, int level, int experience) : base(name, rank) => (Level, Experience) = (level, experience);
    }
}
