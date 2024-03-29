using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace byteStream.JobSeeker.API.Services
{
    public class JobSeekerService : IJobSeekerService
    {

        private readonly AppDbContext dbContext;


        public JobSeekerService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<JobSeekers> CreateAsync(JobSeekers jobSeeker)
        {
            await dbContext.JobSeekerss.AddAsync(jobSeeker);
            await dbContext.SaveChangesAsync();
            return jobSeeker;
        }

        public async Task<JobSeekers?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.JobSeekerss.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            dbContext.JobSeekerss.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<JobSeekers?> GetByIdAsync(Guid id)
        {
            return await dbContext.JobSeekerss.Include(s => s.Experience).Include(s => s.Qualification).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<JobSeekers>> GetUsersAsync(List<Guid> users)
        {
            return await dbContext.JobSeekerss.Where(u => users.Contains(u.Id)).ToListAsync();
        }

        public async Task<JobSeekers?> UpdateAsync(JobSeekers jobSeeker)
        {
            dbContext.JobSeekerss.Update(jobSeeker);
            await dbContext.SaveChangesAsync();
            return jobSeeker;

        }
    }
}