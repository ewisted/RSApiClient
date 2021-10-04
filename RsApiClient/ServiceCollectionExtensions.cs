using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using RSApiClient.GrandExchange;
using RSApiClient.Hiscores;

namespace RSApiClient.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRSClients(
          this IServiceCollection services,
          Action<RSClientOptions>? configureOptions = null, DelegatingHandler? messageHandler = null)
        {
			if (configureOptions == null)
			{
				configureOptions = options => { };
			}

            services.Configure(configureOptions).PostConfigure<RSClientOptions>(options =>
            {
                // Validate options are valid here. Treat this like a 'unit test' of the options.
            });

            // Register lib services here...
            // services.AddScoped<ILibraryService, DefaultLibraryService>();
            var builderOsrsItemApiClient = services.AddHttpClient<OSRSItemApiClient>("OSRSItemApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrl))
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler((svc, m) =>
				{
					var opts = svc.GetRequiredService<IOptions<RSClientOptions>>().Value;
					return GetRetryPolicy(opts.MaxRetries, opts.RetryBackoffFunc);
				});

            if (messageHandler != null)
            {
                builderOsrsItemApiClient.AddHttpMessageHandler(h => messageHandler);
            }

            var builderRS3ItemApiClient = services.AddHttpClient<RS3ItemApiClient>("RS3ItemApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrl))
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler((svc, m) =>
				{
					var opts = svc.GetRequiredService<IOptions<RSClientOptions>>().Value;
					return GetRetryPolicy(opts.MaxRetries, opts.RetryBackoffFunc);
				});

			if (messageHandler != null)
            {
                builderRS3ItemApiClient.AddHttpMessageHandler(h => messageHandler);
            }

			var builderHiscoresApiClient = services.AddHttpClient<HiscoresApiClient>("HiscoresApiClient", (svc, opt) => opt.BaseAddress = new Uri(svc.GetRequiredService<IOptions<RSClientOptions>>().Value.BaseUrl))
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler((svc, m) =>
				{
					var opts = svc.GetRequiredService<IOptions<RSClientOptions>>().Value;
					return GetRetryPolicy(opts.MaxRetries, opts.RetryBackoffFunc);
				});

			if (messageHandler != null)
			{
				builderHiscoresApiClient.AddHttpMessageHandler(h => messageHandler);
			}

            return services;
        }

		static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int maxRetries, Func<int, TimeSpan> retryBackoffFunc)
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
				.OrResult(msg => msg.Content.ReadAsStream().Length == 0)
				.WaitAndRetryAsync(maxRetries, retryBackoffFunc);
		}
	}

    public class RSClientOptions
    {
        public string BaseUrl { get; set; } = "https://secure.runescape.com/";
		public int MaxRetries { get; set; } = 3;
		public Func<int, TimeSpan> RetryBackoffFunc { get; set; } = retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
    }

}