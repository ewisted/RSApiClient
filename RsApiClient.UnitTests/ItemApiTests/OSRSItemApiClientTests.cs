using NUnit.Framework;
using RSApiClient.GrandExchange;
using RSApiClient.GrandExchange.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RSApiClient.Endpoints;

namespace RsApiClient.UnitTests.ItemApiTests
{
    public class OSRSItemApiClientTests : TestBase
    {
        [Test]
        public async Task GetAllItemsTest()
        {
            // Arrange
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            var mockResponse = File.ReadAllText(@"MockData/GetAllItemsOSRSMockResponse.json");
            var dict = new Dictionary<string, string>();
            List<char> chars = new List<char>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                chars.Add(c);
            }
            chars.Add('#');

            for (int i = 0; i < 27; i++)
            {
                string firstRelQuery = EndpointUtils.GetEncodedQueryUrl(ItemEndpoints.GetItemsQueryTemplate, 1, chars[i], 1);
                string firstAbsQuery = $"{TestBaseUrl}{firstRelQuery}";
                dict.Add(firstAbsQuery, mockResponse);

                string secondRelQuery = EndpointUtils.GetEncodedQueryUrl(ItemEndpoints.GetItemsQueryTemplate, 1, chars[i], 2);
                string secondAbsQuery = $"{TestBaseUrl}{secondRelQuery}";
                dict.Add(secondAbsQuery, "{\"Total\": 324, \"Items\": []}");
            }

            var client = GetItemApiClient<OSRSItemApiClient>(dict);

            // Act
            List<ItemPage> pages = new List<ItemPage>();
            await foreach (var page in client.GetAllItemsAsync().WithCancellation(tokenSource.Token))
            {
                pages.Add(page);
            }

            // Assert
            Assert.AreEqual(27, pages.Count);
        }

        [Test]
        public async Task GetAllItemsTest_Cancelled()
        {
            // Arrange
            OSRSItemApiClient client = new OSRSItemApiClient();
            CancellationTokenSource source = new CancellationTokenSource();
            source.Cancel();

            // Act
            var result = await client.GetAllItemsAsync().WithCancellation(source.Token).GetAsyncEnumerator().MoveNextAsync();

            // Assert
            Assert.IsFalse(result);
        }
    }
}