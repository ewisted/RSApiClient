using RSApiClient.ItemApi;
using RSApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.Models
{
    public class TestItemApiClient : ItemApiClientBase
    {
        public TestItemApiClient(HttpClient httpClient) : base(httpClient) { }

        public override IAsyncEnumerable<ItemPage> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
