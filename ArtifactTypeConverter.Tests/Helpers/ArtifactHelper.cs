using System;
using System.Collections.Generic;
using kCura.Relativity.Client;

namespace ArtifactTypeConverter.Tests.Helpers
{
    public static class ArtifactHelper
    {
        public static Artifact CreateTestArtifact()
        {
            //Fields
            var startDateField = new Field { Name = "Start Date", Value = DateTime.Today };
            var endDateField = new Field { Name = "End Date", Value = DateTime.Today.AddDays(1) };
            var dayOfTheWeekField = new Field { Name = "Day of the Week", Value = new Choice(-1, "Monday") };
            var loginField = new Field {Name = "Login", Value = ByteArrayHelper.GetBytes("relativity.admin")};
            var importObjectType = new Field {Name = "Import Object Type", Value = 12345};
            var dontMapField = new Field {Name = "DontMap", Value = ByteArrayHelper.GetBytes("I shouldn't be here")};
                
            //Create Artifact
            var artifact = new Artifact();
            artifact.Fields = new List<Field>
            {
                startDateField,
                endDateField, 
                dayOfTheWeekField,
                loginField,
                importObjectType,
                dontMapField
            };

            return artifact;
        }
    }
}
