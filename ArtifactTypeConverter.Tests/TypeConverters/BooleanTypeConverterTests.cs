using System;
using ArtifactTypeConverter.Exceptions;
using ArtifactTypeConverter.Tests.Helpers;
using ArtifactTypeConverter.TypeConverters.DefaultTypeConverters;
using kCura.Relativity.Client;
using NUnit.Framework;

namespace ArtifactTypeConverter.Tests.TypeConverters
{
    [TestFixture]
    public class BooleanTypeConverterTests
    {
        private ConversionOptions _conversionOptions;
        private BooleanConverter _booleanConverter;

        [SetUp]
        public void SetUp()
        {
            _conversionOptions = new ConversionOptions();
            _conversionOptions.DateTimeFormat = "MM/dd/yyyy H:mm:ss";

            _booleanConverter = new BooleanConverter(_conversionOptions);
        }

        [Test]
        public void Creation_SuccessTest()
        {
            Assert.That(_booleanConverter.DateType, Is.EqualTo(typeof(bool)));
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(ConversionException))]
        public void DateFieldConversion_SuccessTest()
        {
            var currentDate = DateTime.Now;
            var field = new Field { Value = currentDate };
            _booleanConverter.Convert(field);
        }

        [Test]
        public void ChoiceFieldConversion_TrueTest()
        {
            const string expectedResult = "True";

            var field = new Field
            {
                Value = new Choice(-1, expectedResult)
            };

            var result = _booleanConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(bool)));
            Assert.That(result, Is.True, "Result should be equal to the choice name.");
        }

        [Test]
        public void ChoiceFieldConversion_FalseTest()
        {
            const string expectedResult = "False";

            var field = new Field
            {
                Value = new Choice(-1, expectedResult)
            };

            var result = _booleanConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(bool)));
            Assert.That(result, Is.False, "Result should be equal to the choice name.");
        }

        [Test]
        public void IntConversion_OneEqualsTrue()
        {
            var field = new Field
            {
                Value = 1
            };

            var result = _booleanConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(bool)));
            Assert.That(result, Is.True);
        }

        [Test]
        public void IntConversion_ZeroEqualsFalse()
        {
            var field = new Field
            {
                Value = 0
            };

            var result = _booleanConverter.Convert(field);

            Assert.That(result.GetType(), Is.EqualTo(typeof(bool)));
            Assert.That(result, Is.False);
        }

        [Test]
        public void ByteArray_TrueTest()
        {
            var byteArray = ByteArrayHelper.GetBytes("True");

            var field = new Field
            {
                Value = byteArray
            };

            var result = _booleanConverter.Convert(field);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ByteArray_FalseTest()
        {
            var byteArray = ByteArrayHelper.GetBytes("False");

            var field = new Field
            {
                Value = byteArray
            };

            var result = _booleanConverter.Convert(field);

            Assert.That(result, Is.False);
        }

        [Test]
        [ExpectedException(typeof(NullFieldException))]
        public void NullConversion_SuccessTest()
        {
            var field = new Field
            {
                Value = null
            };

            _booleanConverter.Convert(field);
        }
    }
}
