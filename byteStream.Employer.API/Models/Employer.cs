using byteStream.Employer.API.Models;

namespace byteStream.Employer.Api.Models
{
    public class Employeer
    {
        public Guid Id { get; set; }

        public string Organization { get; set; }

        public string OrganizationType { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyPhone { get; set; }

        public int NoOfEmployees { get; set; }

        public int StartYear { get; set; }

        public string About { get; set; }

        public string CreatedBy { get; set; }

        public string ProfileImageUrl {  get; set; }
    }
}
