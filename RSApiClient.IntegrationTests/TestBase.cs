using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RSApiClient.Extensions.DependencyInjection;
using RSApiClient.GrandExchange;
using RSApiClient.Hiscores;

namespace RSApiClient.IntegrationTests
{
    public class TestBase
    {
		protected OSRSItemApiClient? _osrsItemApiClient;
		protected RS3ItemApiClient? _rs3ItemApiClient;
		protected HiscoresApiClient? _hiscoresApiClient;

		[SetUp]
		public void SetupApiClients()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddRSClients();
			var services = serviceCollection.BuildServiceProvider();
			_osrsItemApiClient = services.GetRequiredService<OSRSItemApiClient>();
			_rs3ItemApiClient = services.GetRequiredService<RS3ItemApiClient>();
			_hiscoresApiClient = services.GetRequiredService<HiscoresApiClient>();
		}
    }
}