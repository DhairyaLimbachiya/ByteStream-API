using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace byteStream.JobSeeker.Api.Models
{
	public class Experience
	{
		[Key]
		public Guid Id { get; set; }


		public string CompanyName { get; set; }

		public DateTime StartYear { get; set; }

		public DateTime EndYear { get; set; }

		public string CompanyUrl { get; set; }

		public string Designation { get; set; }

		public string JobDescription { get; set; }

        [ForeignKey("JobSeekers")]
		public Guid UserID { get; set; }

		public JobSeekers JobSeekers { get; set; }
	}
}
