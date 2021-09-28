using RSApiClient.ItemApi;
using RSApiClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace RsApiClient.UnitTests.Models
{
    public class TestItemApiClient : ItemApiClientBase
    {
        public TestItemApiClient(HttpClient httpClient) : base(httpClient) { }

        public override IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
