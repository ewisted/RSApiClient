using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RSApiClient.GrandExchange;
using RSApiClient.Hiscores;

namespace RSApiClient.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRSClients(
          this IServiceCollection services,
          Action<RSClientOptions> configureOptions, DelegatingHandler? messageHandler)
        {
            services.Configure(configureOptions).PostConfigure<RSClientOptions>(options =>
            {
                // Validate options are valid here. Treat this like a 'unit test' of the options.
            });

            // Register lib services here...
            // services.AddScoped<ILibraryService, DefaultLibraryService>();

            var builderOsrsItemApiClient = services.AddHttpClient<OSRSItemApiClient>("OSRSItemApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrl));
            if (messageHandler != null)
            {
                builderOsrsItemApiClient.AddHttpMessageHandler(h => messageHandler);
            }
            var builderRS3ItemApiClient = services.AddHttpClient<RS3ItemApiClient>("RS3ItemApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrl));
            if (messageHandler != null)
            {
                builderRS3ItemApiClient.AddHttpMessageHandler(h => messageHandler);
            }
			var builderHiscoresApiClient = services.AddHttpClient<HiscoresApiClient>("HiscoresApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrl));
			if (messageHandler != null)
			{
				builderHiscoresApiClient.AddHttpMessageHandler(h => messageHandler);
			}

            return services;
        }
    }

    public class RSClientOptions
    {
        public string BaseUrl { get; set; } = "https://secure.runescape.com/";
        public TimeSpan DelayBetweenRetries { get; set; } = TimeSpan.FromSeconds(3);
        public int MaxRetries { get; set; } = 3;
    }

}