using RSApiClient.ItemApi;
using RSApiClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.Models
{
    public class TestItemApiClient : ItemApiClientBase
    {
        public TestItemApiClient(HttpClient httpClient) : base(httpClient) { }

        public override IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> TestSendRequestAsync<T>(HttpMethod method, string queryString, JsonSerializerOptions? options = null, int attempt = 1)
        {
            return SendRequestAsync<T>(method, queryString, options, attempt);
        }
    }
}
