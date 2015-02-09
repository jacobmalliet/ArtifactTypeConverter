using System;
using ArtifactTypeConverter.Exceptions;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters
{
    /// <summary>
    /// FieldTypeConverterBase is the base implementation of IFieldTypeConverter. 
    /// Any class wishing to implement this will need to override multiple convert methods.
    /// </summary>
    public abstract class FieldTypeConverterBase<T> : IFieldTypeConverter
    {
        protected FieldTypeConverterBase(ConversionOptions options)
        {
            Options = options ?? new ConversionOptions();
        }

        protected ConversionOptions Options {get; set; }

        public virtual Type DateType { get { return typeof (T); } }

        public object Convert(Field field)
        {
            if (field.Value == null)
            {
                return HandleNullValue(field.Name);
            }

            var fieldValue = field.Value;
            var fieldType = fieldValue.GetType();

            if (fieldType == typeof(int))
            {
                return Convert((int)fieldValue);
            }

            if (fieldType == typeof(Byte[]))
            {
                return Convert((Byte[])fieldValue);
            }

            if (fieldType == typeof(DateTime))
            {
                return Convert((DateTime)fieldValue);
            }

            if (fieldType == typeof(Choice))
            {
                return Convert((Choice)fieldValue);
            }

            throw new Exception(String.Format("Error converting field from type {0} to type {1}", fieldType, typeof(T)));
        }

        /// <summary>
        /// Converts bool to type T
        /// </summary>
        protected abstract T Convert(bool value);

        /// <summary>
        /// Converts int to type T
        /// </summary>
        protected abstract T Convert(int value);

        /// <summary>
        /// Converts Byte[] to type T
        /// </summary>
        protected abstract T Convert(Byte[] value);

        /// <summary>
        /// Converts DateTime to type T
        /// </summary>
        protected abstract T Convert(DateTime value);

        /// <summary>
        /// Converts Choice value to type T
        /// </summary>
        protected abstract T Convert(Choice value);

        /// <summary>
        /// Default implementation will return the default value of the given type.
        /// </summary>
        /// <returns>Default of the given type</returns>
        protected virtual T HandleNullValue(string fieldName)
        {
            bool canBeNull = !DateType.IsValueType || (Nullable.GetUnderlyingType(DateType) != null);

            if (Options.ErrorOnNonNullableFields && !canBeNull)
            {
                throw  new NullFieldException();
            }

            return default(T);
        }
    }
}
