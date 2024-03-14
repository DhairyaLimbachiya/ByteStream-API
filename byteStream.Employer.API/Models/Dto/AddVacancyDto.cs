namespace byteStream.Employer.API.Models.Dto
{
    public class AddVacancyDto
    {
        public string JobTitle { set; get; }
        public int NoOfVacancies { set; get; }

        public string MinimumQualification { set; get; }

        public string JobDescription { set; get; }

        public string ExperienceRequired { set; get; }

        public DateTime LastDate { set; get; }

        public int MinSalary { set; get; }

        public int MaxSalary { set; get; }
    }
}
