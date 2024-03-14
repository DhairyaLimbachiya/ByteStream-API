using byteStream.Employer.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace byteStream.Employer.API.Models
{
    public class Vacancy
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

        
        //[ForeignKey("Employer")]
        //public Guid EmployerId { get; set; }

        //public  Employeer  employer{ get; set; }
    }
}
