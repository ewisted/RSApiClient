using NUnit.Framework;
using RSApiClient.GrandExchange.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.IntegrationTests.GrandExchangeTests
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
	public class OSRSItemApiClientTests : TestBase
	{
		[Test]
		public async Task GetItemByIdTest()
		{
			// Arrange
			int itemId = 50;

			// Act
			var item = await _osrsItemApiClient.GetItemByIdAsync(itemId);

			// Assert
			Assert.AreEqual(itemId, item.Id);
		}

		[Test]
		public async Task GetItemCatalogueTest()
		{
			// Arrange
			ItemCategory itemCatagory = ItemCategory.Ammo;

			// Act
			var itemCatalogue = await _osrsItemApiClient.GetItemCatalogueAsync(itemCatagory);

			// Assert
			CollectionAssert.IsNotEmpty(itemCatalogue.CharacterCounts);
		}

		[Test]
		public async Task GetItemPageTest()
		{
			// Arrange
			ItemCategory itemCatagory = ItemCategory.Ammo;
			char character = 'a';
			int page = 1;

			// Act
			var itemPage = await _osrsItemApiClient.GetItemPageAsync(itemCatagory, character, page);

			// Assert
			CollectionAssert.IsNotEmpty(itemPage.Items);
		}

		[Test]
		public async Task GetGraphDataForItemTest()
		{
			// Arrange
			int itemId = 50;

			// Act
			var graphData = await _osrsItemApiClient.GetGraphDataForItem(itemId);

			// Assert
			CollectionAssert.IsNotEmpty(graphData.Average);
			CollectionAssert.IsNotEmpty(graphData.Daily);
		}

		[Test]
		public async Task GetAllItemsTest()
		{
			// Arrange
			List<Item> items = new List<Item>();

			// Act
			DateTime startTimeStamp = DateTime.Now;
			Console.WriteLine($"{startTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")} \tStarted get all items test (OSRS)");
			await foreach (var itemPage in _osrsItemApiClient.GetAllItemsAsync())
			{
				items.AddRange(itemPage.Items);
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} \tProccessed: {itemPage.Offset + itemPage.Current} of {itemPage.TotalForCategory}   \tPage: {itemPage.Page} \tCurrent character: {itemPage.Character}");
			}
			DateTime finishTimeStamp = DateTime.Now;
			Console.WriteLine($"{finishTimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")} \tFinished refreshing item metadata in {(finishTimeStamp - startTimeStamp).ToString(@"hh\:mm\:ss", new CultureInfo("en-US"))} (hh:mm:ss)");

			// Assert
			Assert.GreaterOrEqual(3890, items.Count);
		}
	}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
