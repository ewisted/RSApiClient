using NUnit.Framework;
using RsApiClient.UnitTests;
using RSApiClient.Hiscores;
using RSApiClient.Hiscores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.UnitTests.HiscoresTests
{
	public class HiscoresApiClientTests : TestBase
	{
		[Test]
		public async Task GetHiscoresLiteOSRSTest()
		{
			// Arrange
			IHiscoresApiClient mockApiClient = GetApiClient<IHiscoresApiClient>(@"MockData/GetHiscoresLiteOSRSMockResponse.csv");
			HiscoreType type = HiscoreType.OSNormal;
			string playerName = "foo";

			// Act
			HiscoresLiteSnapshot response = await mockApiClient.GetHiscoresLiteAsync(type, playerName);

			// Assert
			Assert.AreEqual(type, response.Type);
			Assert.AreEqual(playerName, response.PlayerName);
			Assert.AreEqual(24, response.Skills.Count());
			Assert.AreEqual(12, response.Activities.Count());
			Assert.AreEqual(47, response.Bosses.Count());
		}

		[Test]
		public async Task GetHiscoresLiteRS3Test()
		{
			// Arrange
			IHiscoresApiClient mockApiClient = GetApiClient<IHiscoresApiClient>(@"MockData/GetHiscoresLiteRS3MockResponse.csv");
			HiscoreType type = HiscoreType.RS3Normal;
			string playerName = "foo";

			// Act
			HiscoresLiteSnapshot response = await mockApiClient.GetHiscoresLiteAsync(type, playerName);

			// Assert
			Assert.AreEqual(type, response.Type);
			Assert.AreEqual(playerName, response.PlayerName);
			Assert.AreEqual(29, response.Skills.Count());
			Assert.AreEqual(30, response.Activities.Count());
			Assert.AreEqual(0, response.Bosses.Count());
		}
	}
}
