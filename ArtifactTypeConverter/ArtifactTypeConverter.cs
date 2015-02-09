using System;
using System.Collections.Generic;
using System.Linq;
using ArtifactTypeConverter.Attributes;
using ArtifactTypeConverter.Exceptions;
using ArtifactTypeConverter.Models;
using ArtifactTypeConverter.TypeConverters;
using ArtifactTypeConverter.TypeConverters.DefaultTypeConverters;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter
{
    public class ArtifactTypeConverter : IArtifactTypeConverter
    {
        private readonly Dictionary<Type, IFieldTypeConverter> _baseFieldConverters;
        private readonly Dictionary<Type, IFieldTypeConverter> _customFieldConverters;

        private readonly ConversionOptions _conversionOptions;

        public ArtifactTypeConverter(ConversionOptions options = null)
        {
            _conversionOptions = options ?? new ConversionOptions();
            _baseFieldConverters = new Dictionary<Type, IFieldTypeConverter>();
            _customFieldConverters = new Dictionary<Type, IFieldTypeConverter>();

            SetupBaseFieldConverters();
        }

        public T Convert<T>(Artifact artifact) where T : new()
        {
            var fieldMappings = CreateFieldMappings(typeof(T));
            var artifactFieldLocations = CreateArtifactFieldLocations(artifact);
            var result = MapArtifact<T>(artifact, fieldMappings, artifactFieldLocations);
            return result;
        }

        public IEnumerable<T> Convert<T>(List<Artifact> artifacts) where T : new()
        {
            var fieldMappings = CreateFieldMappings(typeof(T)).ToList();
            var artifactFieldLocations = CreateArtifactFieldLocations(artifacts.First());
            
            return artifacts.Select(artifact => MapArtifact<T>(artifact, fieldMappings, artifactFieldLocations)).ToList();
        }

        public T GetFieldValue<T>(Field field)
        {
            return (T) GetFieldValue(field, typeof (T));
        }

        private object GetFieldValue(Field field, Type returnType)
        {
            //Check for nullable return type
            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof (Nullable<>))
            {
                //return type is nullable
                
                //Check if the field is null. If so we should handle this differently then non-nullable types.
                if (field.Value == null)
                {
                    return null;
                }
                   
                //Object is not null so we can continue like normal. Get the underlying type so we can select the correct converter.
                returnType = Nullable.GetUnderlyingType(returnType);
            }

            //Check for enum
            if (returnType.IsEnum)
            {
                //Check for custom field converter
                if (_customFieldConverters.ContainsKey(returnType))
                    return _customFieldConverters[returnType].Convert(field);

                //Fallback to default implementation
                var enumConverter = (EnumConverter)_baseFieldConverters[typeof(Enum)];
                    return enumConverter.Convert(field, returnType);
            }


            //Basic implementation from here on out. Start by checking for custom converters
            if (_customFieldConverters.ContainsKey(returnType))
                return _customFieldConverters[returnType].Convert(field);

            if (_baseFieldConverters.ContainsKey(returnType))
                return  _baseFieldConverters[returnType].Convert(field);

            //No converters match the given type. Throw a conversion error
            throw new ConversionException(String.Format("Error converting field '{0}' to type '{1}'. No converters were available for this type. You can create your own by implementing FieldTypeConverterBase.", field.Name, returnType));
        }

        public void RegisterCustomConverter(IFieldTypeConverter fieldTypeConverter)
        {
            _customFieldConverters[fieldTypeConverter.DateType] = fieldTypeConverter;
        }

        private void SetupBaseFieldConverters()
        {
            _baseFieldConverters.Add(typeof(String), new StringConverter(_conversionOptions));
            _baseFieldConverters.Add(typeof(DateTime), new DateTimeConverter(_conversionOptions));
            _baseFieldConverters.Add(typeof(Enum), new EnumConverter(_conversionOptions));
            _baseFieldConverters.Add(typeof(Boolean), new BooleanConverter(_conversionOptions));
            _baseFieldConverters.Add(typeof(Int32), new IntConverter(_conversionOptions));
        }

        private IEnumerable<PropertyMappings> CreateFieldMappings(Type returnType)
        {
            var mappings = new List<PropertyMappings>();

            var fields = returnType.GetProperties();
            
            foreach (var fieldInfo in fields.Where(field => !Attribute.IsDefined(field, typeof(IgnoreFieldMappingAttribute))))
            {
                //Check if attribute is defined.
                String fieldName;
                var attr = fieldInfo.GetCustomAttributes(typeof(FieldNameAttribute), false);
                if (attr.Length > 0)
                {
                    fieldName = ((FieldNameAttribute)attr[0]).Name;
                }
                else
                {
                    fieldName = fieldInfo.Name;
                }

                mappings.Add(new PropertyMappings(fieldName, fieldInfo));
            }

            return mappings;
        }

        private T MapArtifact<T>(Artifact artifact, IEnumerable<PropertyMappings> mappings, IDictionary<string, int> artifactFieldLocations) where T : new()
        {
            var result = new T();

            foreach (var mapping in mappings)
            {
                var artifactField = artifact.Fields[artifactFieldLocations[mapping.FieldName]];
                var fieldValue = GetFieldValue(artifactField, mapping.PropertyInfo.PropertyType);

                mapping.SetterDelegate.Invoke(result, fieldValue);
            }

            return result;
        }

        private IDictionary<string, int> CreateArtifactFieldLocations(Artifact artifact)
        {
            var dictionary = new Dictionary<string, int>();
            for (int i = 0; i < artifact.Fields.Count; i++)
            {
                var field = artifact.Fields[i];
                dictionary.Add(field.Name, i);
            }
            return dictionary;
        }
    }
}
