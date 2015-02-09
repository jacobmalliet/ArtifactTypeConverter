using System;
using System.Globalization;
using System.Text;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters.DefaultTypeConverters
{
    public class StringConverter : FieldTypeConverterBase<String>
    {
        public StringConverter(ConversionOptions options) : base(options)
        {
        }

        protected override string Convert(bool value)
        {
            return value.ToString();
        }

        protected override string Convert(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        protected override string Convert(byte[] value)
        {
            return Encoding.UTF8.GetString(value);
        }

        protected override string Convert(DateTime value)
        {
            return value.ToString(Options.DateTimeFormat);
        }

        protected override string Convert(Choice value)
        {
            return value.Name;
        }
    }
}
