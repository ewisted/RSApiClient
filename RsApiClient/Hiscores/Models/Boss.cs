using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record Boss : RankingSnapshot
    {
        public int Kills { get; init; }
		public Boss(string name, int rank, int kills) : base(name, rank) => Kills = kills;
    }
}
