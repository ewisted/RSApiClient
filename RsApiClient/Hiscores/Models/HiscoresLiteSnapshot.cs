using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
	public record HiscoresLiteSnapshot
	{
		public string PlayerName { get; init; }
		public HiscoreType Type { get; init; }
		public DateTime SnapshotTimeStampUtc { get; init; }
		public IEnumerable<Skill> Skills {  get; init; }
		public IEnumerable<Activity> Activities { get; init; }
		public IEnumerable<Boss> Bosses { get; init; }

		public HiscoresLiteSnapshot(string playerName, HiscoreType type, DateTime snapshotTimeStampUtc, IEnumerable<Skill> skills, IEnumerable<Activity> activities, IEnumerable<Boss> bosses) =>
			(PlayerName, Type, SnapshotTimeStampUtc, Skills, Activities, Bosses) = (playerName, type, snapshotTimeStampUtc, skills, activities, bosses);
	}
}
