using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Models;
using Microsoft.EntityFrameworkCore;

namespace byteStream.Employer.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employeer> Employers { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<UserVacancyRequests> UserVacancyRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
