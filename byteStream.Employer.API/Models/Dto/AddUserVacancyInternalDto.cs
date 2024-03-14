using System.ComponentModel.DataAnnotations.Schema;

namespace byteStream.Employer.Api.Models.Dto
{
	public class AddUserVacancyInternalDto
	{
		public Guid Id { get; set; }

		public DateTime AppliedDate { get; set; }

		public Guid VacancyID { get; set; }

	}
}
