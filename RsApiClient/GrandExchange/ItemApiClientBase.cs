using RSApiClient.Base;
using RSApiClient.Endpoints;
using RSApiClient.JsonConverters;
using RSApiClient.GrandExchange.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RSApiClient.GrandExchange
{
    public abstract class ItemApiClientBase : ApiClientBase, IItemApiClient
    {
        protected ItemApiClientBase(string baseUrl) : this(new HttpClient() { BaseAddress = new Uri(baseUrl) }) { }
        protected ItemApiClientBase(HttpClient httpClient) : base (httpClient) { }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            string query = EndpointUtils.GetEncodedQueryUrl(ItemEndpoints.GetItemByIdQueryTemplate, itemId);
            ItemResult result = await SendRequestAsync<ItemResult>(HttpMethod.Get, query);
            return result.Item;
        }

        public abstract IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default);

        public async Task<ItemPage> GetItemPageAsync(ItemCategory category, char character, int page)
        {
            string query = EndpointUtils.GetEncodedQueryUrl(ItemEndpoints.GetItemsQueryTemplate, (int)category, character, page);
            ItemPage result = await SendRequestAsync<ItemPage>(HttpMethod.Get, query);
            result.Current = result.Items.Count();
            result.Category = category;
            result.Character = character;
            result.Page = page;
            return result;
        }

        public async Task<ItemGraphData> GetGraphDataForItem(int itemId)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new GraphDataJsonConverter());

            string query = EndpointUtils.GetEncodedQueryUrl(ItemEndpoints.GetItemGraphDataQueryTemplate, itemId);
            ItemGraphData result = await SendRequestAsync<ItemGraphData>(HttpMethod.Get, query, options);
            return result;
        }

        protected IEnumerable<char> GetCharsForGetAllItemsQuery()
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                yield return c;
            }

            yield return '#';
        }
    }
}
