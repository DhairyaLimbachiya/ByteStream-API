using System.ComponentModel.DataAnnotations.Schema;

namespace byteStream.JobSeeker.Api.Models
{
	public class UserVacancyRequests
	{
		public Guid Id { get; set; }

		public Guid VacancyID { get; set; }
		public DateTime AppliedDate { get; set; }

		[ForeignKey("JobSeekers")]
		public Guid UserID { get; set; }

		public JobSeekers JobSeekers { get; set; }
	}
}
