using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RSApiClient.GrandExchange;

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

            var builderOsrsApiClient = services.AddHttpClient<OSRSItemApiClient>("OSRSItemApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrlOSRS));
            if (messageHandler != null)
            {
                builderOsrsApiClient.AddHttpMessageHandler(h => messageHandler);
            }
            var builderRS3ApiClient = services.AddHttpClient<RS3ItemApiClient>("RS3ItemApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrlRS3));
            if (messageHandler != null)
            {
                builderRS3ApiClient.AddHttpMessageHandler(h => messageHandler);
            }

            return services;
        }
    }

    public class RSClientOptions
    {
        public string BaseUrlRS3 { get; set; } = "https://secure.runescape.com/m=itemdb_rs/api/";
        public string BaseUrlOSRS { get; set; } = "https://secure.runescape.com/m=itemdb_oldschool/api/";
        public TimeSpan DelayBetweenRetries { get; set; } = TimeSpan.FromSeconds(3);
        public int MaxRetries { get; set; } = 3;
    }

}