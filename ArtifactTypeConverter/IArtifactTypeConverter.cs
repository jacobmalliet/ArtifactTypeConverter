using System.Collections.Generic;
using ArtifactTypeConverter.TypeConverters;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter
{
    public interface IArtifactTypeConverter
    {
        T Convert<T>(Artifact artifact) where T : new();

        IEnumerable<T> Convert<T>(List<Artifact> artifacts) where T : new();

        T GetFieldValue<T>(Field field);

        void RegisterCustomConverter(IFieldTypeConverter fieldTypeConverter);
    }
}
