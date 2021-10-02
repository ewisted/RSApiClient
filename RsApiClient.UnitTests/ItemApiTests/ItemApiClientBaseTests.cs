using NUnit.Framework;
using System.Threading.Tasks;
using RsApiClient.UnitTests.Models;
using RSApiClient.Models;
using System.Text.Json;
using RSApiClient.Base;

namespace RsApiClient.UnitTests.ItemApiTests
{
    public class ItemApiClientBaseTests : TestBase
    {
        [Test]
        public async Task GetItemByIdTest()
        {
            // Arrange
            TestItemApiClient mockApiClient = GetItemApiClient<TestItemApiClient>(@"MockData/GetItemByIdMockResponse.json");

            // Act
            Item item = await mockApiClient.GetItemByIdAsync(50);

            // Assert
            Assert.AreEqual(50, item.Id);
        }

        [Test]
        public async Task GetGraphDataForItemTest()
        {
            // Arrange
            TestItemApiClient mockApiClient = GetItemApiClient<TestItemApiClient>(@"MockData/GetGraphDataForItemMockResponse.json");

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
            TestItemApiClient mockApiClient = GetItemApiClient<TestItemApiClient>();

            // Act
            var exception = Assert.ThrowsAsync<HttpRetryLimitExceededException>(async () => { await mockApiClient.GetItemByIdAsync(50); });

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception?.InnerException);
            Assert.AreEqual(typeof(JsonException), exception?.InnerException?.GetType());
        }
    }
}
