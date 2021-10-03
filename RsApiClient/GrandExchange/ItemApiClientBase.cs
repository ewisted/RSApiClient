using RSApiClient.Base;
using RSApiClient.Endpoints;
using RSApiClient.JsonConverters;
using RSApiClient.GrandExchange.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RSApiClient.Extensions.DependencyInjection;

namespace RSApiClient.GrandExchange
{
    public abstract class ItemApiClientBase : ApiClientBase, IItemApiClient
    {
        protected ItemApiClientBase(HttpClient httpClient, IOptions<RSClientOptions> options) : base (httpClient, options) { }

        public async Task<Item> GetItemByIdAsync(int itemId, CancellationToken cancellationToken = default)
        {
            string query = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemByIdQueryTemplate, itemId);
            ItemResult result = await SendRequestAsync<ItemResult>(HttpMethod.Get, query, null, cancellationToken);
            return result.Item;
        }

        public async Task<CategoryItemCatalogue> GetItemCatalogueAsync(ItemCategory category, CancellationToken cancellationToken = default)
        {
            string query = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemCatalogueQueryTemplate, (int)category);
            CategoryItemCatalogue result = await SendRequestAsync<CategoryItemCatalogue>(HttpMethod.Get, query, null, cancellationToken);
            return result;
        }

        public abstract IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default);

        public async Task<ItemPage> GetItemPageAsync(ItemCategory category, char character, int page, CancellationToken cancellationToken = default)
        {
            string query = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemsQueryTemplate, (int)category, character, page);
            ItemPage result = await SendRequestAsync<ItemPage>(HttpMethod.Get, query, null, cancellationToken);
            result.Current = result.Items.Count();
            result.Category = category;
            result.Character = character;
            result.Page = page;
            return result;
        }

        public async Task<ItemGraphData> GetGraphDataForItem(int itemId, CancellationToken cancellationToken = default)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new GraphDataJsonConverter());

            string query = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemGraphDataQueryTemplate, itemId);
            ItemGraphData result = await SendRequestAsync<ItemGraphData>(HttpMethod.Get, query, options, cancellationToken);
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
