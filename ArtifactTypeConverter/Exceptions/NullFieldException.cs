using System;

namespace ArtifactTypeConverter.Exceptions
{
    public class NullFieldException : Exception
    {
        private const string DefaultErrorMessage = "The field supplied was null. To disable these warnings disable the flag within ConversionOptions.";

         public NullFieldException() : this(DefaultErrorMessage)
        {
        }

        public NullFieldException(string message) : base(message)
        {
        }

        public NullFieldException(string message, Exception inner): base(message, inner)
        {
        }
    }
}
