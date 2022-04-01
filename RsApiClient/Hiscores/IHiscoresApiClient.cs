using RSApiClient.Hiscores.Models;

namespace RSApiClient.Hiscores
{
	public interface IHiscoresApiClient
	{
		Task<HiscoresLiteSnapshot> GetHiscoresLiteAsync(HiscoreType type, string playerName);
	}
}
