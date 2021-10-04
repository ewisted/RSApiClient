using RSApiClient.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RSApiClient.GrandExchange.Models;
using System.Runtime.CompilerServices;

namespace RSApiClient.GrandExchange
{
    public class RS3ItemApiClient : ItemApiClientBase
    {
        public RS3ItemApiClient(HttpClient httpClient, IOptions<RSClientOptions> options) : base(httpClient, options, "itemdb_rs") { }

        public override async IAsyncEnumerable<ItemPage> GetAllItemsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            bool currentCategoryFinished = false;
            int page = 1;
            foreach (ItemCategory category in Enum.GetValues(typeof(ItemCategory)).OfType<ItemCategory>())
            {
                CategoryItemCatalogue catalogueInfo = await GetItemCatalogueAsync(category, cancellationToken);
                int categoryOffset = 0;
                foreach (char character in GetCharsForGetAllItemsQuery())
                {
                    if (cancellationToken != default)
                    {
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException)
                        {
                            yield break;
                        }
                    }

                    if (currentCategoryFinished)
                    {
                        currentCategoryFinished = false;
                        break;
                    }

                    CharacterItemCount characterInfo = catalogueInfo.CharacterCounts.Single(c => c.Letter == character);
                    for (int i = 0; categoryOffset < characterInfo.Count; i++)
                    {
                        ItemPage result = await GetItemPageAsync(category, character, i + 1, cancellationToken);
                        if (!result.Items.Any())
                        {
                            break;
                        }

                        result.Offset = categoryOffset;
                        result.Page = page;
                        result.Character = character;
                        result.Category = category;

                        yield return result;


                        if (categoryOffset + result.Current < result.TotalForCategory)
                        {
                            categoryOffset += result.Current;
                            page++;
                        }
                        else
                        {
                            currentCategoryFinished = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}
