using RSApiClient.Base;
using RSApiClient.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RSApiClient.Extensions.DependencyInjection;
using RSApiClient.Hiscores.Models;
using RSApiClient.Extensions;

namespace RSApiClient.Hiscores
{
    public class HiscoresApiClient : ApiClientBase
    {
        public HiscoresApiClient(HttpClient httpClient, IOptions<RSClientOptions> options) : base(httpClient, options, "") { }

		public async Task<HiscoresLiteSnapshot> GetHiscoresLiteAsync(HiscoreType type, string playerName)
		{
			string queryString = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.Hiscores_GetHiscoresLiteQueryTemplate, type.GetModuleStringValue(), playerName);
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, queryString);
			HttpResponseMessage response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			string responseString = await response.Content.ReadAsStringAsync() ?? throw new Exception("Response content was empty");
			if ((int)type < 3)
			{
				return GetRS3SnapshotFromCsv(type, playerName, responseString);
			}
			else
			{
				return GetOSRSSnapshotFromCsv(type, playerName, responseString);
			}
		}

		private HiscoresLiteSnapshot GetOSRSSnapshotFromCsv(HiscoreType type, string playerName, string csv)
		{
			List<Skill> skills = new List<Skill>();
			List<Activity> activities = new List<Activity>();
			List<Boss> bosses = new List<Boss>();
			DateTime snapshotTimestampUtc = DateTime.UtcNow;

			using (StringReader reader = new StringReader(csv))
			{
				for (int i = 0; i < 83; i++)
				{
					string line = reader.ReadLine() ?? throw new Exception($"Error parsing snapshot from csv: line {i + 1} was null");
					string[] lineParts = line.Split(',');
					string name = ((OSRSHiscoresCsvMapping)i).GetNameString();
					int rank = int.Parse(lineParts[0]);
					if (i < 24)
					{
						int level = int.Parse(lineParts[1]);
						int experience = int.Parse(lineParts[2]);
						skills.Add(new Skill(name, rank, level, experience));
					}
					else if (i < 36)
					{
						int score = int.Parse(lineParts[1]);
						activities.Add(new Activity(name, rank, score));
					}
					else
					{
						int kills = int.Parse(lineParts[1]);
						bosses.Add(new Boss(name, rank, kills));
					}
				}
			}

			return new HiscoresLiteSnapshot(playerName, type, snapshotTimestampUtc, skills, activities, bosses);
		}

		private HiscoresLiteSnapshot GetRS3SnapshotFromCsv(HiscoreType type, string playerName, string csv)
		{
			List<Skill> skills = new List<Skill>();
			List<Activity> activities = new List<Activity>();
			List<Boss> bosses = new List<Boss>();
			DateTime snapshotTimestampUtc = DateTime.UtcNow;

			using (StringReader reader = new StringReader(csv))
			{
				for (int i = 0; i < 59; i++)
				{
					string line = reader.ReadLine() ?? throw new Exception($"Error parsing snapshot from csv: line {i + 1} was null");
					string[] lineParts = line.Split(',');
					string name = ((RS3HiscoresCsvMapping)i).GetNameString();
					int rank = int.Parse(lineParts[0]);
					if (i < 29)
					{
						int level = int.Parse(lineParts[1]);
						int experience = int.Parse(lineParts[2]);
						skills.Add(new Skill(name, rank, level, experience));
					}
					else
					{
						int score = int.Parse(lineParts[1]);
						activities.Add(new Activity(name, rank, score));
					}
				}
			}

			return new HiscoresLiteSnapshot(playerName, type, snapshotTimestampUtc, skills, activities, bosses);
		}
	}
}
