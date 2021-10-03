using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record Activity
    {
        public int Rank { get; init; }
        public int Score { get; init; }
        public Activity(int rank, int score) => (Rank, Score) = (rank, score);
    }
}
