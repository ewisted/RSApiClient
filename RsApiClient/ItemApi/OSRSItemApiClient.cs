using RSApiClient.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RSApiClient.ItemApi
{
    public class OSRSItemApiClient : ItemApiClientBase
    {
        public const string DefaultBaseUrl = "https://secure.runescape.com/m=itemdb_oldschool/api/";

        public OSRSItemApiClient() : base(DefaultBaseUrl) { }
        public OSRSItemApiClient(HttpClient httpClient) : base(httpClient) { }

        public override async IAsyncEnumerable<ItemPage> GetAllItemsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            int offset = 0;
            int page = 1;
            foreach (string character in GetCharsForGetAllItemsQuery())
            {
                for (int i = 0; true; i++)
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

                    ItemPage result = await GetItemPageAsync(1, character, i + 1);
                    if (!result.Items.Any())
                    {
                        break;
                    }

                    result.Offset = offset;
                    result.Page = page;
                    result.Current = result.Items.Count();
                    result.Character = character != "%23" ? character : "0-9";

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
