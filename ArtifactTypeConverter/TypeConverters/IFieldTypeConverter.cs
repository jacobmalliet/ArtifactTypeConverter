using System;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.TypeConverters
{
    public interface IFieldTypeConverter
    {
        Type DateType { get; }

        object Convert(Field field);
    }
}
