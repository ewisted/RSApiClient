using NUnit.Framework;
using RSApiClient.JsonConverters;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsApiClient.UnitTests.JsonConverterTests
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public class PriceJsonConverterTests
    {
        [Test]
        public void ReadTest_String()
        {
            // Arrange
            var testJson = "\"14.5k\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("14.5k", result);
        }

        [Test]
        public void ReadTest_Int()
        {
            // Arrange
            var testJson = "42";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(testJson, result);
        }

        [Test]
        public void ReadTest_Fail()
        {
            // Arrange
            var testJson = "{\"foo\":\"bar\"}";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            Exception? exception = null;
            try
            {
                converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.NotNull(exception);
            Assert.AreEqual("Invalid token when attempting to deserialize price: StartObject", exception.Message);
        }

        [Test]
        public async Task WriteTest_Success()
        {
            // Arrange
            var testObj = "14.5k";
            PriceJsonConverter converter = new PriceJsonConverter();
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
            Assert.AreEqual("\"14.5k\"", result);
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
