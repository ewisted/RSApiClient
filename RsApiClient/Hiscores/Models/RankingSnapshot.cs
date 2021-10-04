using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
	public abstract record RankingSnapshot
	{
		public string Name { get; init; }
		public int Rank { get; init; }
		protected RankingSnapshot(string name, int rank) => (Name, Rank) = (name, rank);
	}
}
