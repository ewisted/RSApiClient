using Moq;
using RSApiClient.GrandExchange;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Moq.Protected;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using RSApiClient.Extensions.DependencyInjection;
using RSApiClient.Base;

namespace RsApiClient.UnitTests
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    public class TestBase
    {
        protected const string TestBaseUrl = "https://foo.bar/";

        protected T GetApiClient<T>(string? mockDataPath = null, string mockContentString = "") where T : ApiClientBase
        {
            if (!string.IsNullOrWhiteSpace(mockDataPath))
            {
                mockContentString = File.ReadAllText(mockDataPath);
            }

            var httpMessageHandlerMock = new Mock<DelegatingHandler>();
            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockContentString)
            };

            httpMessageHandlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(mockResponse);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddRSClients(options =>
            {
                options.DelayBetweenRetries = TimeSpan.Zero;
                options.BaseUrl = TestBaseUrl;
            }, httpMessageHandlerMock.Object);
            
            var services = serviceCollection.BuildServiceProvider();
            var instance = services.GetRequiredService<T>();

            //HttpClient mockHttpClient = new HttpClient(httpMessageHandlerMock.Object)
            //{
            //    BaseAddress = new Uri(TestBaseUrl)
            //};

            //T instance = (T)Activator.CreateInstance(typeof(T), mockHttpClient) ?? throw new ArgumentException();

            //instance.DelayBetweenRetries = TimeSpan.Zero;

            return instance;
        }

        protected T GetApiClient<T>(IDictionary<string, string> queryResponses) where T : ApiClientBase
        {
            var httpMessageHandlerMock = new Mock<DelegatingHandler>();
            foreach ((string query, string responseJson) in queryResponses)
            {
                HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseJson)
                };

                httpMessageHandlerMock
                  .Protected()
                  .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.RequestUri != null ? m.RequestUri.AbsoluteUri == query : string.IsNullOrWhiteSpace(query)),
                    ItExpr.IsAny<CancellationToken>())
                  .ReturnsAsync(mockResponse);
            }

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddRSClients(options =>
            {
                options.DelayBetweenRetries = TimeSpan.Zero;
                options.BaseUrl = TestBaseUrl;
            }, httpMessageHandlerMock.Object);
            var services = serviceCollection.BuildServiceProvider();
            var instance = services.GetRequiredService<T>();

            //HttpClient mockHttpClient = new HttpClient(httpMessageHandlerMock.Object)
            //{
            //    BaseAddress = new Uri("https://foo.bar/")
            //};


            //T instance = (T)Activator.CreateInstance(typeof(T), mockHttpClient) ?? throw new ArgumentException();

            //instance.DelayBetweenRetries = TimeSpan.Zero;

            return instance;
        }


    }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
}
