using System.ComponentModel.DataAnnotations.Schema;

namespace byteStream.JobSeeker.Api.Models.Dto
{
    public class UserVacancyDto
    {
		public Guid Id { get; set; }

		public Guid VacancyID { get; set; }
		public DateTime AppliedDate { get; set; }
		public Guid UserID { get; set; }

		

	}
}
