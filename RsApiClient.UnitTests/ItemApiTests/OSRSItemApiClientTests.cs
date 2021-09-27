using NUnit.Framework;
using RSApiClient.ItemApi;
using RSApiClient.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests
{
    public class OSRSItemApiClientTests : TestBase
    {
        [Test]
        public async Task GetAllItemsTest()
        {
            // Arrange
            var mockResponse = File.ReadAllText(@"MockData/GetAllItemsOSRSMockResponse.json");
            var dict = new Dictionary<string, string>();
            List<string> chars = new List<string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                chars.Add(c.ToString().ToLower());
            }
            chars.Add("%23");

            for (int i = 0; i < 27; i++)
            {
                string firstRelQuery = string.Format(ItemEndpoints.GetAllItemsQueryTemplate, 1, chars[i], 1);
                string firstAbsQuery = $"{TestBaseUrl}{firstRelQuery}";
                dict.Add(firstAbsQuery, mockResponse);

                string secondRelQuery = string.Format(ItemEndpoints.GetAllItemsQueryTemplate, 1, chars[i], 2);
                string secondAbsQuery = $"{TestBaseUrl}{secondRelQuery}";
                dict.Add(secondAbsQuery, "{\"Total\": 324, \"Items\": []}");
            }

            var client = GetItemApiClient<OSRSItemApiClient>(dict);

            // Act
            List<ItemPage> pages = new List<ItemPage>();
            await foreach (var page in client.GetAllItemsAsync())
            {
                pages.Add(page);
            }

            // Assert
            Assert.AreEqual(27, pages.Count);
        }
    }
}