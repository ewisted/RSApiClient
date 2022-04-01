using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RSApiClient.Extensions.DependencyInjection;
using RSApiClient.GrandExchange;
using RSApiClient.Hiscores;

namespace RSApiClient.IntegrationTests
{
    public class TestBase
    {
		protected IOSRSItemApiClient? _osrsItemApiClient;
		protected IRS3ItemApiClient? _rs3ItemApiClient;
		protected IHiscoresApiClient? _hiscoresApiClient;

		[SetUp]
		public void SetupApiClients()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddRSClients();
			var services = serviceCollection.BuildServiceProvider();
			_osrsItemApiClient = services.GetRequiredService<IOSRSItemApiClient>();
			_rs3ItemApiClient = services.GetRequiredService<IRS3ItemApiClient>();
			_hiscoresApiClient = services.GetRequiredService<IHiscoresApiClient>();
		}
    }
}