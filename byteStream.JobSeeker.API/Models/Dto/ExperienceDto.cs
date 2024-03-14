using static System.Runtime.InteropServices.JavaScript.JSType;

namespace byteStream.JobSeeker.Api.Models.Dto
{
	public class ExperienceDto
	{
		public Guid Id { get; set; }

		public string CompanyName { get; set; }

		public DateTime StartYear { get; set; }

		public DateTime EndYear { get; set; }

		public string CompanyUrl { get; set; }

		public string Designation { get; set; }

		public string JobDescription { get; set; }
	}
}
