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
    public class IsMembersJsonConverterTests
    {
        [Test]
        public void ReadTest_Success()
        {
            // Arrange
            var testJson = "\"true\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            IsMembersJsonConverter converter = new IsMembersJsonConverter();

            // Act
            var result = converter.Read(ref reader, typeof(bool), new JsonSerializerOptions());

            // Assert
            Assert.True(result);
        }

        [Test]
        public void ReadTest_FailToParse()
        {
            // Arrange
            var testJson = "\"foo\"";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            IsMembersJsonConverter converter = new IsMembersJsonConverter();

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
            Assert.AreEqual("String 'foo' was not recognized as a valid Boolean.", exception.Message);
        }

        [Test]
        public void ReadTest_NullValue()
        {
            // Arrange
            var testJson = "null";
            var bytes = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(testJson));
            Utf8JsonReader reader = new Utf8JsonReader(bytes);
            reader.Read();
            IsMembersJsonConverter converter = new IsMembersJsonConverter();

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
            Assert.AreEqual("Error deserializing IsMembers property: value was null", exception.Message);
        }

        [Test]
        public async Task WriteTest_Success()
        {
            // Arrange
            bool testObj = true;
            IsMembersJsonConverter converter = new IsMembersJsonConverter();
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
            Assert.True(bool.Parse(result));
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}
