using System;
using ArtifactTypeConverter.Tests.Helpers;
using ArtifactTypeConverter.TypeConverters.DefaultTypeConverters;
using kCura.Relativity.Client;
using NUnit.Framework;

namespace ArtifactTypeConverter.Tests.TypeConverters
{
    [TestFixture]
    public class StringTypeConverterTests
    {
        private ConversionOptions _conversionOptions;
        private StringConverter _stringConverter;

        [SetUp]
        public void SetUp()
        {
            _conversionOptions = new ConversionOptions();
            _conversionOptions.DateTimeFormat = "MM/dd/yyyy H:mm:ss";

            _stringConverter = new StringConverter(_conversionOptions);
        }

        [Test]
        public void Creation_SuccessTest()
        {
            Assert.That(_stringConverter.DateType, Is.EqualTo(typeof(string)));
        }

        [Test]
        public void DateFieldConversion_SuccessTest()
        {
            var currentDate = DateTime.Now;
            var expectedResult = currentDate.ToString(_conversionOptions.DateTimeFormat);

            var field = new Field { Value = currentDate };

            var result = _stringConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(string)));
            Assert.That(result, Is.EqualTo(expectedResult), "Result should be formatted according to the conversion options.");
        }

        [Test]
        public void ChoiceFieldConversion_SuccessTest()
        {
            const string expectedResult = "Test Data";

            var field = new Field
            {
                Value = new Choice(-1, expectedResult)
            };

            var result = _stringConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(string)));
            Assert.That(result, Is.EqualTo(expectedResult), "Result should be equal to the choice name.");
        }

        [Test]
        public void IntConversion_SuccessTest()
        {
            var field = new Field
            {
                Value = 999
            };

            var result = _stringConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(string)));
            Assert.That(result, Is.EqualTo("999"), "Result should be 999");
        }

        [Test]
        public void ByteArray_SuccessTest()
        {
            var byteArray = ByteArrayHelper.GetBytes("Test String");

            var field = new Field
            {
                Value = byteArray
            };

            var result = _stringConverter.Convert(field);

            Assert.That(result, Is.EqualTo("Test String"));
        }

        [Test]
        public void NullConversion_SuccessTest()
        {
            var field = new Field
            {
                Value = null
            };

            var result = _stringConverter.Convert(field);

            Assert.That(result, Is.Null);
        }
    }
}
