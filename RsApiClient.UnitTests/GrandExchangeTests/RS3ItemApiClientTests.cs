using NUnit.Framework;
using RSApiClient.Endpoints;
using RSApiClient.GrandExchange;
using RSApiClient.GrandExchange.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.GrandExchangeTests
{
    public class RS3ItemApiClientTests : TestBase
    {
        [Test]
        public async Task GetAllItemsTest()
        {
            // Arrange
            var mockResponse = File.ReadAllText(@"MockData/GetAllItemsRS3MockResponse.json");
            var dict = new Dictionary<string, string>();

			var catalogueResponse = File.ReadAllText(@"MockData/GetItemCatalogueMockResponse.json");

			for (int i = 0; i <= 41; i++)
            {
				string catalogueRelQuery = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemCatalogueQueryTemplate, "itemdb_rs", i);
				string catalogueAbsQuery = $"{TestBaseUrl}{catalogueRelQuery}";
				dict.Add(catalogueAbsQuery, catalogueResponse);

				string firstRelQuery = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemsQueryTemplate, "itemdb_rs", i, "a", 1);
                string firstAbsQuery = $"{TestBaseUrl}{firstRelQuery}";
                dict.Add(firstAbsQuery, mockResponse);

                string secondRelQuery = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemsQueryTemplate, "itemdb_rs", i, "a", 2);
                string secondAbsQuery = $"{TestBaseUrl}{secondRelQuery}";
                dict.Add(secondAbsQuery, "{\"Total\": 12, \"Items\": []}");

                string thirdRelQuery = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.GrandExchange_GetItemsQueryTemplate, "itemdb_rs", i, "b", 1);
                string thirdAbsQuery = $"{TestBaseUrl}{thirdRelQuery}";
                dict.Add(thirdAbsQuery, mockResponse);
            }

            var client = GetApiClient<IRS3ItemApiClient>(dict);

            // Act
            List<ItemPage> pages = new List<ItemPage>();
            await foreach (var page in client.GetAllItemsAsync())
            {
                pages.Add(page);
            }

            // Assert
            Assert.AreEqual(84, pages.Count);
        }

        [Test]
        public void GetAllItemsTest_Cancelled()
        {
            // Arrange
            IRS3ItemApiClient client = GetApiClient<IRS3ItemApiClient>();
            CancellationTokenSource source = new CancellationTokenSource();
            source.Cancel();

			// Assert
			Assert.ThrowsAsync<TaskCanceledException>(async () => await client.GetAllItemsAsync().WithCancellation(source.Token).GetAsyncEnumerator().MoveNextAsync());
		}
    }
}
