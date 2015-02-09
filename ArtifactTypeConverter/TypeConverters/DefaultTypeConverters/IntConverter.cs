using System;
using System.Text;
using ArtifactTypeConverter.Exceptions;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters.DefaultTypeConverters
{
    public class IntConverter : FieldTypeConverterBase<int>
    {
        public IntConverter(ConversionOptions options) : base(options)
        {
        }

        protected override int Convert(bool value)
        {
            return System.Convert.ToInt32(value);
        }

        protected override int Convert(int value)
        {
            return value;
        }

        protected override int Convert(byte[] value)
        {
            var valueString = Encoding.UTF8.GetString(value);
            return Convert(valueString);
        }

        protected override int Convert(DateTime value)
        {
            throw new ConversionException("Error converting type 'DateTime' to type 'int'");
        }

        protected override int Convert(Choice value)
        {
            var valueString = value.Name;
            return Convert(valueString);
        }

        private int Convert(string value)
        {
            int intValue;

            var result = int.TryParse(value, out intValue);

            if (result)
                return intValue;

            throw new ConversionException(String.Format("Error converting '{0}' to type 'int'", value));
        }
    }
}
