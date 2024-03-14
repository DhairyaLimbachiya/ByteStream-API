namespace byteStream.Employer.API.Models.Dto
{
    public class VacancyDto
    {

        public Guid Id { set; get; }
        public string JobTitle { set; get; }

        public string PublishedBy { set; get; }

        public DateTime PublishedDate { set; get; }

        public int NoOfVacancies { set; get; }

        public string MinimumQualification { set; get; }

        public string JobDescription { set; get; }

        public string ExperienceRequired { set; get; }

        public DateTime LastDate { set; get; }

        public int MinSalary { set; get; }

        public int MaxSalary { set; get; }
        public bool? AlreadyApplied { get; set; }

    }
}
