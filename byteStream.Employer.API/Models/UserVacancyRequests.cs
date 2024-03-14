using byteStream.Employer.API.Models;
using byteStream.JobSeeker.Api.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace byteStream.Employer.Api.Models
{
    public class UserVacancyRequests
    {
        public Guid Id { get; set; }
        public Guid VacancyId { get; set; }
        [ForeignKey("VacancyId")]
        [ValidateNever]
        public Vacancy? Vacancy { get; set; }
        public Guid UserId { get; set; }
        [NotMapped]
        public UserDto? User { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
