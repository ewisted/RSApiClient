using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record Activity : RankingSnapshot
    {
        public int Score { get; init; }
		public Activity(string name, int rank, int score) : base(name, rank) => Score = score;
    }
}
