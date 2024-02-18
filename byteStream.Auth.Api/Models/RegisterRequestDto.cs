
using System.ComponentModel.DataAnnotations;

namespace byteStream.Auth.Api.Models
{
	public class RegisterRequestDto
	{
		[Required]
		public string FullName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid Email.")]
		public string Email { get; set; }
		[Required]
		[RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
		public string PhoneNumber { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[AllowedValues("employer", "jobseeker")]
		public string UserType { get; set; }
	}
}
