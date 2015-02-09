using System;
using System.Text;
using ArtifactTypeConverter.Exceptions;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters.DefaultTypeConverters
{
    public class EnumConverter : IFieldTypeConverter
    {
        private readonly ConversionOptions _options;

        public EnumConverter(ConversionOptions options)
        {
            _options = options;
        }

        public Type DateType { get { return typeof (Enum); } }

        public object Convert(Field field)
        {
            throw new InvalidOperationException("No type defined for EnumConvertion. Use the generic method instead.");
        }

        public object Convert(Field field, Type returnType)
        {
            if (field.Value == null)
            {
                if (_options.ErrorOnNonNullableFields)
                    throw new NullFieldException();

                return Activator.CreateInstance(returnType);
            }

            var fieldValue = field.Value;
            var fieldType = fieldValue.GetType();

            if (fieldType == typeof(int))
            {
                return Enum.ToObject(returnType , fieldValue);
            }

            if (fieldType == typeof(Byte[]))
            {
                var stringValue = Encoding.UTF8.GetString((Byte[]) fieldValue);
                return Convert(stringValue, returnType);
            }

            if (fieldType == typeof(Choice))
            {
                var stringValue = ((Choice) fieldValue).Name;
                return Convert(stringValue, returnType);
            }
            
            throw new ConversionException(String.Format("Error converting field from type {0} to type {1}", fieldType, returnType));
        }

        private static object Convert(string value, Type returnType)
        {
            try
            {
                var result = Enum.Parse(returnType, value);
                return result;
            }
            catch (Exception)
            {
                throw new ConversionException(String.Format("Error converting '{0}' to type '{1}'", value, returnType));
            }
        }
    }
}
