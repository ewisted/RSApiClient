using RSApiClient.GrandExchange.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RSApiClient.Extensions.DependencyInjection;

namespace RSApiClient.GrandExchange
{
    public class OSRSItemApiClient : ItemApiClientBase
    {
        public OSRSItemApiClient(HttpClient httpClient, IOptions<RSClientOptions> options) : base(httpClient, options) { }

        public override async IAsyncEnumerable<ItemPage> GetAllItemsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
			CategoryItemCatalogue catalogueInfo = await GetItemCatalogueAsync(ItemCategory.Ammo, cancellationToken);

			int offset = 0;
			int page = 1;
			foreach (char character in GetCharsForGetAllItemsQuery())
			{
				CharacterItemCount characterItemCount = catalogueInfo.CharacterCounts.Single(c => c.Letter == character);
				int currentCount = 0;
				for (int i = 0; currentCount < characterItemCount.Count; i++)
				{
					ItemPage result = await GetItemPageAsync(ItemCategory.Ammo, character, i + 1, cancellationToken);
					if (!result.Items.Any())
					{
						break;
					}

					currentCount += result.Current;
					result.Offset = offset;
					result.Page = page;
					result.Character = character;

					yield return result;

					if (offset + result.Current < result.TotalForCategory)
					{
						offset += result.Current;
						page++;
					}
					else
					{
						yield break;
					}
				}
			}
        }
    }
}
