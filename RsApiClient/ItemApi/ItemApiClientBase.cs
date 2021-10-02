using RSApiClient.JsonConverters;
using RSApiClient.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RSApiClient.ItemApi
{
    public abstract class ItemApiClientBase : IItemApiClient
    {
        private readonly HttpClient _httpClient;

        protected ItemApiClientBase(string baseUrl) : this(new HttpClient() { BaseAddress = new Uri(baseUrl) }) { }

        protected ItemApiClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
            DelayBetweenRetries = TimeSpan.FromSeconds(3);
            MaxRetries = 3;
        }

        /// <summary>
        /// Time to wait before retrying failed requests. Default is 3 seconds.
        /// </summary>
        public TimeSpan DelayBetweenRetries { get; set; }
        /// <summary>
        /// Maximum number of times to retry a failed request. Defaul is 3.
        /// </summary>
        public int MaxRetries { get; set; }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            string query = string.Format(ItemEndpoints.GetItemByIdQueryTemplate, itemId);
            ItemResult result = await SendRequestAsync<ItemResult>(HttpMethod.Get, query);
            return result.Item;
        }

        public abstract IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default);

        public async Task<ItemPage> GetItemPageAsync(int category, string character, int page)
        {
            string query = string.Format(ItemEndpoints.GetItemsQueryTemplate, category, character, page);
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

            string query = string.Format(ItemEndpoints.GetItemGraphDataQueryTemplate, itemId);
            ItemGraphData result = await SendRequestAsync<ItemGraphData>(HttpMethod.Get, query, options);
            return result;
        }

        protected async Task<T> SendRequestAsync<T>(HttpMethod method, string queryString, JsonSerializerOptions? options = null, int attempt = 1)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, queryString);

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string jsonContent = await response.Content.ReadAsStringAsync();
                T result = JsonSerializer.Deserialize<T>(jsonContent, options) ?? throw new JsonException("Deserialization result was null");

                return result;
            }
            catch (Exception ex)
            {
                if (attempt <= MaxRetries)
                {
                    await Task.Delay(DelayBetweenRetries);
                    return await SendRequestAsync<T>(method, queryString, options, attempt+1);
                }
                else
                {
                    throw new HttpRetryLimitExceededException(MaxRetries, method, $"{_httpClient.BaseAddress}{queryString}", ex);
                }
            }
        }

        protected IEnumerable<string> GetCharsForGetAllItemsQuery()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                yield return c.ToString().ToLower();
            }

            yield return "%23";
        }
    }

    public class HttpRetryLimitExceededException : Exception
    {
        public int MaxRetries { get; set; }
        public HttpMethod Method { get; set; }
        public string Query { get; set; }

        public HttpRetryLimitExceededException(int maxRetries, HttpMethod method, string query, Exception innerException) 
            : base($"Max retries ({maxRetries}) exceeded for request: {method} {query}", innerException)
        {
            MaxRetries = maxRetries;
            Method = method;
            Query = query;
        }
    }
}
