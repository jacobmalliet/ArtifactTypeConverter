using System;

namespace ArtifactTypeConverter.Exceptions
{
    public class ConversionException : Exception
    {
        private const string DefaultErrorMessage = "Error converting field to the given type. To disable these warnings disable the flag within ConversionOptions.";

        public ConversionException() : base(DefaultErrorMessage)
        {
        }

        public ConversionException(string message): base(message)
        {
        }

        public ConversionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
