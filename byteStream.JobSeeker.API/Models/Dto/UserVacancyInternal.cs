namespace byteStream.JobSeeker.Api.Models.Dto
{
	public class UserVacancyInternal
	{
		public Guid Id { get; set; }

		public DateTime AppliedDate { get; set; }
		public Guid UserID { get; set; }

		public Guid VacancyID { get; set; }
	}
}
