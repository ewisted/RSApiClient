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
        public void ReadTest_KString()
        {
            // Arrange
            var testJson = "\"14.5k\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(14500, result);
        }

        [Test]
        public void ReadTest_MString()
        {
            // Arrange
            var testJson = "\"22.5m\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(22500000, result);
        }

        [Test]
        public void ReadTest_BString()
        {
            // Arrange
            var testJson = "\"1.4b\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(1400000000, result);
        }

        [Test]
        public void ReadTest_PercentPositiveString()
        {
            // Arrange
            var testJson = "\"+65.0%\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(65, result);
        }

        [Test]
        public void ReadTest_PercentNegativeString()
        {
            // Arrange
            var testJson = "\"-65.0%\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(-65, result);
        }

        [Test]
        public void ReadTest_CommaString()
        {
            // Arrange
            var testJson = "\"- 1,472\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            PriceJsonConverter converter = new PriceJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(1472, result);
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
            var result = converter.Read(ref reader, typeof(int), new JsonSerializerOptions());

            // Assert
            Assert.AreEqual(42, result);
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
                converter.Read(ref reader, typeof(int), new JsonSerializerOptions());
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
            var testObj = 42;
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
            Assert.AreEqual("42", result);
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
