namespace byteStream.JobSeeker.Api.Models.Dto
{
	public class QualificationDto
	{
		public Guid Id { get; set; }


		public string QualificationName { get; set; }

		public string University { get; set; }

		public double YearOfCompletion { get; set; }

		public string GradeOrScore { get; set; }
	}
}
