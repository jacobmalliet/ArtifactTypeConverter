using System;

namespace ArtifactTypeConverter
{
    public class ConversionOptions
    {
        public ConversionOptions()
        {
            DateTimeFormat = "MM/dd/yy H:mm:ss";
            ErrorOnNonNullableFields = true;
        }


        public bool ErrorOnNonNullableFields { get; set; }

        public String DateTimeFormat { get; set; }

    }
}
