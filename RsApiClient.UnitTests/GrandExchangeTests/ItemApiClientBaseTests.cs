﻿using NUnit.Framework;
using System.Threading.Tasks;
using RSApiClient.GrandExchange.Models;
using System.Text.Json;
using RSApiClient.Base;
using System.Linq;
using RSApiClient.GrandExchange;
using System;

namespace RsApiClient.UnitTests.GrandExchangeTests
{
    public class ItemApiClientBaseTests : TestBase
    {
        [Test]
        public async Task GetItemByIdTest()
        {
            // Arrange
            RS3ItemApiClient mockApiClient = GetApiClient<RS3ItemApiClient>(@"MockData/GetItemByIdMockResponse.json");

            // Act
            Item item = await mockApiClient.GetItemByIdAsync(50);

            // Assert
            Assert.AreEqual(50, item.Id);
        }

        [Test]
        public async Task GetItemCatalogueTest()
        {
            // Arrange
            OSRSItemApiClient mockApiClient = GetApiClient<OSRSItemApiClient>(@"MockData/GetItemCatalogueMockResponse.json");

            // Act
            CategoryItemCatalogue catalogue = await mockApiClient.GetItemCatalogueAsync(ItemCategory.Ammo);

            // Assert
            Assert.AreEqual(27, catalogue.CharacterCounts.Count());
        }

        [Test]
        public async Task GetGraphDataForItemTest()
        {
            // Arrange
            RS3ItemApiClient mockApiClient = GetApiClient<RS3ItemApiClient>(@"MockData/GetGraphDataForItemMockResponse.json");

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
            OSRSItemApiClient mockApiClient = GetApiClient<OSRSItemApiClient>();
			DateTime startTimeStamp = DateTime.Now;

            // Act
            var exception = Assert.ThrowsAsync<JsonException>(async () => { await mockApiClient.GetItemByIdAsync(50); });
			DateTime endTimeStamp = DateTime.Now;

            // Assert
            Assert.Greater(endTimeStamp - startTimeStamp, TimeSpan.FromSeconds(3));
        }
    }
}