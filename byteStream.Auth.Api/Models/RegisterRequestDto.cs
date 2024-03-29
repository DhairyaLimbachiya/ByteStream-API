
using System.ComponentModel.DataAnnotations;

namespace byteStream.Auth.Api.Models
{
	public class RegisterRequestDto
	{
		[Required]
		public string FullName { get; set; }

		[Required]

		public string Email { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string UserType { get; set; }
	}
}
