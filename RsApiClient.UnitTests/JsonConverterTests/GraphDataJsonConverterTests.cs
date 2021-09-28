using Moq;
using NUnit.Framework;
using RSApiClient.JsonConverters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.JsonConverterTests
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public class GraphDataJsonConverterTests
    {
        [Test]
        public void ReadTest_NonStartObjectToken()
        {
            // Arrange
            var testJson = "0";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            GraphDataJsonConverter converter = new GraphDataJsonConverter();

            // Act
            JsonException? exception = null;
            try
            {
                converter.Read(ref reader, typeof(Dictionary<DateTimeOffset, int>), new JsonSerializerOptions());
            }
            catch (JsonException ex)
            {
                exception = ex;
            }

            // Assert
            Assert.NotNull(exception);
            Assert.AreEqual("Expected a start object token but got Number", exception.Message);
        }

        [Test]
        public void ReadTest_Success()
        {
            // Arrange
            var testJson = "{\"1616976000000\": 2455639}";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            GraphDataJsonConverter converter = new GraphDataJsonConverter();

            // Act
            var dict = converter.Read(ref reader, typeof(Dictionary<DateTimeOffset, int>), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(dict);
            CollectionAssert.IsNotEmpty(dict);
        }

        [Test]
        public void ReadTest_InvalidPropertyName()
        {
            // Arrange
            var testJson = "{\"foo\": 0}";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            GraphDataJsonConverter converter = new GraphDataJsonConverter();

            // Act
            JsonException? exception = null;
            try
            {
                converter.Read(ref reader, typeof(Dictionary<DateTimeOffset, int>), new JsonSerializerOptions());
            }
            catch (JsonException ex)
            {
                exception = ex;
            }

            // Assert
            Assert.NotNull(exception);
            Assert.AreEqual("Unable to convert \"foo\" to DateTimeOffset", exception.Message);
        }

        [Test]
        public async Task WriteTest_Success()
        {
            // Arrange
            var testObj = new Dictionary<DateTimeOffset, int>();
            testObj.Add(DateTimeOffset.FromUnixTimeMilliseconds(1616976000000), 2455639);
            GraphDataJsonConverter converter = new GraphDataJsonConverter();
            MemoryStream stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);

            // Act
            converter.Write(writer, testObj, new JsonSerializerOptions());
            writer.Flush();

            stream.Position = 0;

            string? result = null;
            using (var reader = new StreamReader(stream))
            {
                result = await reader.ReadToEndAsync();
            }

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("{\"1616976000000\":2455639}", result);
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
