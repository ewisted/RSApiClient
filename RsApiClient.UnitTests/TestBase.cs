using Moq;
using RSApiClient.ItemApi;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Moq.Protected;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace RsApiClient.UnitTests
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    public class TestBase
    {
        protected const string TestBaseUrl = "https://foo.bar/";

        protected T GetItemApiClient<T>(string? mockDataPath = null) where T : ItemApiClientBase
        {
            string mockContentString = "";
            if (!string.IsNullOrWhiteSpace(mockDataPath))
            {
                mockContentString = File.ReadAllText(mockDataPath);
            }

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
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

            HttpClient mockHttpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri(TestBaseUrl)
            };

            T instance = (T)Activator.CreateInstance(typeof(T), mockHttpClient) ?? throw new ArgumentException();

            instance.DelayBetweenRetries = TimeSpan.Zero;

            return instance;
        }

        protected T GetItemApiClient<T>(IDictionary<string, string> queryResponses) where T : ItemApiClientBase
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
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

            HttpClient mockHttpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://foo.bar/")
            };


            T instance = (T)Activator.CreateInstance(typeof(T), mockHttpClient) ?? throw new ArgumentException();

            instance.DelayBetweenRetries = TimeSpan.Zero;

            return instance;
        }


    }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
}
