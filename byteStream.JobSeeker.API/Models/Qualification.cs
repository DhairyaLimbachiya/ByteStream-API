using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace byteStream.JobSeeker.Api.Models
{
	public class Qualification
	{
		public Guid Id { get; set; }


		public string QualificationName { get; set; }

		public string University { get; set; }

		public double YearOfCompletion { get; set; }

		public string GradeOrScore { get; set; }
	
		[ForeignKey("JobSeekers")]
		public Guid UserID
		{ get; set; }

		public JobSeekers JobSeekers { get; set; }
	}
}
