using System;
using System.Text;
using ArtifactTypeConverter.Exceptions;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters.DefaultTypeConverters
{
    public class BooleanConverter : FieldTypeConverterBase<bool>
    {
        public BooleanConverter(ConversionOptions options) : base(options)
        {
        }

        protected override bool Convert(bool value)
        {
            return value;
        }

        protected override bool Convert(int value)
        {
            return System.Convert.ToBoolean(value);
        }

        protected override bool Convert(byte[] value)
        {
            var valueString = Encoding.UTF8.GetString(value);
            return System.Convert.ToBoolean(valueString);
        }

        protected override bool Convert(DateTime value)
        {
            throw new ConversionException("Cannot convert type 'DateTime' to type 'Boolean'");
        }

        protected override bool Convert(Choice value)
        {
            return System.Convert.ToBoolean(value.Name);
        }
    }
}
