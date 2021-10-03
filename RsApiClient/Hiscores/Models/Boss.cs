using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public record Boss
    {
        public int Rank { get; init; }
        public int Kills { get; init; }
        public Boss(int rank, int kills) => (Rank, Kills) = (rank, kills);
    }
}
