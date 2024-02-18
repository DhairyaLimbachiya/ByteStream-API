using byteStream.Employer.API.Models;

namespace byteStream.Employer.Api.Models
{
    public class Employeer
    {
        public Guid Id { get; set; }

        public string Organization { get; set; }

        public string OrgnizationType { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyPhone { get; set; }

        public string NoOfEmployees { get; set; }

        public string StartYear { get; set; }

        public string About { get; set; }

        public string CreatedBy { get; set; }

        public ICollection<Vacancy> Vacancy { get; set; }
    }
}
