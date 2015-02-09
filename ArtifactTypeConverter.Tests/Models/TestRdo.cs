using System;
using ArtifactTypeConverter.Attributes;

namespace ArtifactTypeConverter.Tests.Models
{
	public class TestRdo
	{
        [FieldName("Start Date")]
		public DateTime StartDate { get; set; }

        [FieldName("End Date")]
		public DateTime? EndDate { get; set; }

        [FieldName("Day of the Week")]
		public DayOfWeek DayOfTheWeek { get; set; }

		public String Login { get; set; }

        [FieldName("Import Object Type")]
		public int ImportObjectType { get; set; }

        [IgnoreFieldMapping]
        public String DontMap { get; set; }
	}
}
