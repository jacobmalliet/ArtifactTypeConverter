using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ArtifactTypeConverter.Models
{
    internal class PropertyMappings
    {
        public PropertyMappings(String fieldName, PropertyInfo propertyInfo)
        {
            FieldName = fieldName;
            PropertyInfo = propertyInfo;
            SetterDelegate = GetValueSetter(propertyInfo);
        }

        public String FieldName { get; private set; }

        public PropertyInfo PropertyInfo { get; private set; }

        public Action<object, object> SetterDelegate { get; set; }

        public static Action<object, object> GetValueSetter(PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(typeof(object), "i");
            var argument = Expression.Parameter(typeof(object), "a");
            var setterCall = Expression.Call(Expression.Convert(instance, propertyInfo.DeclaringType), propertyInfo.GetSetMethod(), Expression.Convert(argument, propertyInfo.PropertyType));
            return Expression.Lambda<Action<object, object>>(setterCall, instance, argument).Compile();
        }
    }
}
