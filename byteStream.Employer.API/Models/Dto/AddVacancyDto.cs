namespace byteStream.Employer.API.Models.Dto
{
    public class AddVacancyDto
    {
        public int NoOfVacancies { set; get; }

        public string MinimumQualification { set; get; }

        public string JobDescription { set; get; }

        public int ExperienceRequired { set; get; }

        public DateTime LastDate { set; get; }

        public int MinSalary { set; get; }

        public int MaxSalary { set; get; }
    }
}
