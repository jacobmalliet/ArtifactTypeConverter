using System;

namespace ArtifactTypeConverter.Attributes
{
     [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class FieldNameAttribute : Attribute
    {
        public FieldNameAttribute(String name)
        {
            Name = name;
        }

        public string Name;
    }
}
