using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StockTradingAnalysis.Core.Services;
using System;

namespace StockTradingAnalysis.Core.Tests.Services
{
    [TestClass]
    public class JsonServiceTests
    {
        private class TestClass
        {
            public string TestProperty { get; set; }
        }

        private class CompatipleOtherTestClass
        {
            public string TestProperty { get; set; }
        }

        [TestMethod]
        public void DeserializeShouldThrowIfValueIsNull()
        {
            Action act = () => new JsonSerializerService().Deserialize<TestClass>(null);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void DeserializeShouldNotThrowIfValueIsEmpty()
        {
            Action act = () => new JsonSerializerService().Deserialize<TestClass>(string.Empty);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void DeserializeShouldDeserializeAnObject()
        {
            var test = new TestClass() { TestProperty = "This is a test" };
            var testSerialized = JsonConvert.SerializeObject(test);

            var testDeserialized = new JsonSerializerService().Deserialize<TestClass>(testSerialized);

            test.TestProperty.Should().BeEquivalentTo(testDeserialized.TestProperty);
        }

        [TestMethod]
        public void SerializeShouldNotThrowIfValueIsNull()
        {
            Action act = () => new JsonSerializerService().Serialize(null);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void SerializedStringShouldBeEmptyIfObjectWasNull()
        {
            var result = new JsonSerializerService().Serialize(null);
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void DeserializeShouldWorkWithDifferentCLassesIfStructureIsTheSame()
        {
            var test = new TestClass() { TestProperty = "This is a test" };
            var testSerialized = JsonConvert.SerializeObject(test);

            var testDeserialized = new JsonSerializerService().Deserialize<CompatipleOtherTestClass>(testSerialized);
        }
    }
}
