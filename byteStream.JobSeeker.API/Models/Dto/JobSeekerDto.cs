namespace byteStream.JobSeeker.Api.Models.Dto
{
	public class JobSeekerDto
	{
        public Guid Id { get; set; }

        public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public string Address { get; set; }

		public double TotalExperience { get; set; }

		public int ExpectedSalary { get; set; }

		public DateTime DOB { get; set; }

        public string? ResumeURL { get; set; }

        public string? ProfileImgURL { get; set; }


    }
}
