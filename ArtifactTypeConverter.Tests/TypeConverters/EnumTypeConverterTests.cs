using System;
using ArtifactTypeConverter.Exceptions;
using ArtifactTypeConverter.Tests.Helpers;
using ArtifactTypeConverter.TypeConverters.DefaultTypeConverters;
using kCura.Relativity.Client;
using NUnit.Framework;

namespace ArtifactTypeConverter.Tests.TypeConverters
{
    [TestFixture]
    public class EnumTypeConverterTests
    {
        private ConversionOptions _conversionOptions;
        private EnumConverter _enumConverter;

        [SetUp]
        public void SetUp()
        {
            _conversionOptions = new ConversionOptions();

            _enumConverter = new EnumConverter(_conversionOptions);
        }

        [Test]
        public void Creation_SuccessTest()
        {
            Assert.That(_enumConverter.DateType, Is.EqualTo(typeof(Enum)));
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(ConversionException))]
        public void DateFieldConversion_SuccessTest()
        {
            var currentDate = DateTime.Now;
            var field = new Field { Value = currentDate };
            _enumConverter.Convert(field, typeof(DayOfWeek));
        }

        [Test]
        public void ChoiceFieldConversion_SuccessTest()
        {
            string expectedResult = DayOfWeek.Friday.ToString();

            var field = new Field
            {
                Value = new Choice(-1, expectedResult)
            };

            var result = _enumConverter.Convert(field, typeof(DayOfWeek));

            Assert.That(result.GetType(), Is.EqualTo(typeof(DayOfWeek)));
            Assert.That(result, Is.EqualTo(DayOfWeek.Friday), "Result should be equal to the choice name.");
        }

        [Test]
        public void IntConversion_SuccessTest()
        {
            var field = new Field
            {
                Value = 1
            };

            var result = _enumConverter.Convert(field, typeof(DayOfWeek));

            Assert.That(result.GetType(), Is.EqualTo(typeof(DayOfWeek)));
            Assert.That(result, Is.EqualTo(DayOfWeek.Monday), "Result should be Monday");
        }

        [Test]
        public void ByteArray_SuccessTest()
        {
            var byteArray = ByteArrayHelper.GetBytes("Tuesday");

            var field = new Field
            {
                Value = byteArray
            };

            var result = _enumConverter.Convert(field, typeof(DayOfWeek));

            Assert.That(result, Is.EqualTo(DayOfWeek.Tuesday));
        }

        [Test]
        [ExpectedException(typeof(NullFieldException))]
        public void NullConversion_SuccessTest()
        {
            var field = new Field
            {
                Value = null
            };
            _enumConverter.Convert(field, typeof(DayOfWeek));
        }
    }
}
