using NUnit.Framework;
using RSApiClient.JsonConverters;
using RSApiClient.GrandExchange.Models;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.JsonConverterTests
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public class TrendTypeJsonConverterTests
    {
        [Test]
        public void ReadTest_Negative()
        {
            // Arrange
            var testJson = "\"negative\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            TrendTypeJsonConverter converter = new TrendTypeJsonConverter();

            // Act
            TrendType result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(TrendType.Negative, result);
        }

        [Test]
        public void ReadTest_Positive()
        {
            // Arrange
            var testJson = "\"positive\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            TrendTypeJsonConverter converter = new TrendTypeJsonConverter();

            // Act
            TrendType result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(TrendType.Positive, result);
        }

        [Test]
        public void ReadTest_Neutral()
        {
            // Arrange
            var testJson = "\"neutral\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            TrendTypeJsonConverter converter = new TrendTypeJsonConverter();

            // Act
            TrendType result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(TrendType.Neutral, result);
        }

        [Test]
        public void ReadTest_Default()
        {
            // Arrange
            var testJson = "\"foo\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            TrendTypeJsonConverter converter = new TrendTypeJsonConverter();

            // Act
            TrendType result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(TrendType.None, result);
        }

        [Test]
        public async Task WriteTest_Success()
        {
            // Arrange
            var testObj = TrendType.Positive;
            TrendTypeJsonConverter converter = new TrendTypeJsonConverter();
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
            Assert.AreEqual("\"positive\"", result);
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
