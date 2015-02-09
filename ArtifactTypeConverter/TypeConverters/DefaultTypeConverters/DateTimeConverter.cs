using System;
using System.Text;
using ArtifactTypeConverter.Exceptions;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters.DefaultTypeConverters
{
    public class DateTimeConverter : FieldTypeConverterBase<DateTime>
    {
        public DateTimeConverter(ConversionOptions options) : base(options)
        {
        }

        protected override DateTime Convert(bool value)
        {
            throw new ConversionException("Cannot convert type 'Boolean' to type 'DateTime'");
        }

        protected override DateTime Convert(int value)
        {
            throw new ConversionException("Cannot convert type 'Int' to type 'DateTime'");
        }

        protected override DateTime Convert(byte[] value)
        {
            var dateString = Encoding.UTF8.GetString(value);
            return Convert(dateString);
        }

        protected override DateTime Convert(DateTime value)
        {
            return value;
        }

        protected override DateTime Convert(Choice value)
        {
            var dateString = value.Name;
            return Convert(dateString);
        }

        private static DateTime Convert(String dateString)
        {
            DateTime date;
            var result = DateTime.TryParse(dateString, out date);
            if (result)
                return date;

            //Error converting value
            throw new ConversionException("Error convert type 'Byte[]' to type 'DateTime'");
        }
    }
}
