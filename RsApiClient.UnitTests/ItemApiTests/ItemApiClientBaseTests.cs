using NUnit.Framework;
using System.Threading.Tasks;
using RSApiClient.GrandExchange.Models;
using System.Text.Json;
using RSApiClient.Base;
using System.Linq;
using RSApiClient.GrandExchange;

namespace RsApiClient.UnitTests.ItemApiTests
{
    public class ItemApiClientBaseTests : TestBase
    {
        [Test]
        public async Task GetItemByIdTest()
        {
            // Arrange
            RS3ItemApiClient mockApiClient = GetItemApiClient<RS3ItemApiClient>(@"MockData/GetItemByIdMockResponse.json");

            // Act
            Item item = await mockApiClient.GetItemByIdAsync(50);

            // Assert
            Assert.AreEqual(50, item.Id);
        }

        [Test]
        public async Task GetItemCatalogueTest()
        {
            // Arrange
            OSRSItemApiClient mockApiClient = GetItemApiClient<OSRSItemApiClient>(@"MockData/GetItemCatalogueMockResponse.json");

            // Act
            CategoryItemCatalogue catalogue = await mockApiClient.GetItemCatalogueAsync(ItemCategory.Ammo);

            // Assert
            Assert.AreEqual(27, catalogue.CharacterCounts.Count());
        }

        [Test]
        public async Task GetGraphDataForItemTest()
        {
            // Arrange
            RS3ItemApiClient mockApiClient = GetItemApiClient<RS3ItemApiClient>(@"MockData/GetGraphDataForItemMockResponse.json");

            // Act
            ItemGraphData graphData = await mockApiClient.GetGraphDataForItem(4151);

            // Assert
            CollectionAssert.IsNotEmpty(graphData.Average);
            CollectionAssert.IsNotEmpty(graphData.Daily);
        }

        [Test]
        public void EmptyResponseContentTest()
        {
            // Arrange
            OSRSItemApiClient mockApiClient = GetItemApiClient<OSRSItemApiClient>();

            // Act
            var exception = Assert.ThrowsAsync<HttpRetryLimitExceededException>(async () => { await mockApiClient.GetItemByIdAsync(50); });

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception?.InnerException);
            Assert.AreEqual(typeof(JsonException), exception?.InnerException?.GetType());
        }
    }
}
