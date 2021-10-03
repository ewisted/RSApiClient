using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record Skill
    {
        public int Rank { get; init; }
        public int Level { get; init; }
        public int Experience { get; init; }
        public Skill(int rank, int level, int experience) => (Rank, Level, Experience) = (rank, level, experience);
    }
}
