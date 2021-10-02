using RSApiClient.Models;
using System.Runtime.CompilerServices;

namespace RSApiClient.ItemApi
{
    public class RS3ItemApiClient : ItemApiClientBase
    {
        public const string DefaultBaseUrl = "https://secure.runescape.com/m=itemdb_rs/api/";

        public RS3ItemApiClient() : base(DefaultBaseUrl) { }
        public RS3ItemApiClient(HttpClient httpClient) : base(httpClient) { }

        public override async IAsyncEnumerable<ItemPage> GetAllItemsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            bool currentCategoryFinished = false;
            int page = 1;
            foreach (ItemCategory category in Enum.GetValues(typeof(ItemCategory)).OfType<ItemCategory>())
            {
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

                    for (int i = 0; true; i++)
                    {
                        ItemPage result = await GetItemPageAsync(category, character, i + 1);
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
