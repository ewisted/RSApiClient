using NUnit.Framework;
using RSApiClient.ItemApi;
using RSApiClient.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.ItemApiTests
{
    public class RS3ItemApiClientTests : TestBase
    {
        [Test]
        public async Task GetAllItemsTest()
        {
            // Arrange
            var mockResponse = File.ReadAllText(@"MockData/GetAllItemsRS3MockResponse.json");
            var dict = new Dictionary<string, string>();

            for (int i = 0; i <= 41; i++)
            {
                string firstRelQuery = string.Format(ItemEndpoints.GetItemsQueryTemplate, i, "a", 1);
                string firstAbsQuery = $"{TestBaseUrl}{firstRelQuery}";
                dict.Add(firstAbsQuery, mockResponse);

                string secondRelQuery = string.Format(ItemEndpoints.GetItemsQueryTemplate, i, "a", 2);
                string secondAbsQuery = $"{TestBaseUrl}{secondRelQuery}";
                dict.Add(secondAbsQuery, "{\"Total\": 12, \"Items\": []}");

                string thirdRelQuery = string.Format(ItemEndpoints.GetItemsQueryTemplate, i, "b", 1);
                string thirdAbsQuery = $"{TestBaseUrl}{thirdRelQuery}";
                dict.Add(thirdAbsQuery, mockResponse);
            }

            var client = GetItemApiClient<RS3ItemApiClient>(dict);

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
        public async Task GetAllItemsTest_Cancelled()
        {
            // Arrange
            RS3ItemApiClient client = new RS3ItemApiClient();
            CancellationTokenSource source = new CancellationTokenSource();
            source.Cancel();

            // Act
            var result = await client.GetAllItemsAsync().WithCancellation(source.Token).GetAsyncEnumerator().MoveNextAsync();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
