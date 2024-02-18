﻿using Microsoft.AspNetCore.Identity;

namespace byteStream.Auth.Api.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }

		public string UserType { get; set; }

	}
}
