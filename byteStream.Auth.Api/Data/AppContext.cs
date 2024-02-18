using byteStream.Auth.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace byteStream.Auth.Api.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			List<IdentityRole> roles = new List<IdentityRole>()
	{
		new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
		new IdentityRole { Name = "Employer", NormalizedName = "EMPLOYER" },
		new IdentityRole { Name = "JobSeeker", NormalizedName = "JOBSEEKER" }
	};
			modelBuilder.Entity<IdentityRole>().HasData(roles);
		}


	}
}
