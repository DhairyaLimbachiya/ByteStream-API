
using byteStream.JobSeeker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace byteStream.JobSeeker.Api.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<JobSeekers> JobSeekerss { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
		
	}
}
