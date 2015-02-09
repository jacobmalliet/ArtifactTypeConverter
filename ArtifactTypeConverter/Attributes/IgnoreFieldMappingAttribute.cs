using System;

namespace ArtifactTypeConverter.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class IgnoreFieldMappingAttribute : Attribute
    {
    }
}
