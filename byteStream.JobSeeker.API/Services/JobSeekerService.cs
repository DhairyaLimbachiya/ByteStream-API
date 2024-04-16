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

        /// <summary>
        /// To add a new JobSeeker to the Database
        /// </summary>
        /// <param name="jobSeeker"></param>
        /// <returns></returns>
        public async Task<JobSeekers> CreateAsync(JobSeekers jobSeeker)
        {
            await dbContext.JobSeekerss.AddAsync(jobSeeker);
            await dbContext.SaveChangesAsync();
            return jobSeeker;
        }

        /// <summary>
        /// To delete jobseeker profile from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JobSeekers?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.JobSeekerss.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            dbContext.JobSeekerss.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }

        /// <summary>
        /// To get jobseeker details from the Database based on its 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JobSeekers?> GetByIdAsync(Guid id)
        {
            return await dbContext.JobSeekerss.Include(s => s.Experience).Include(s => s.Qualification).FirstOrDefaultAsync(x => x.Id == id);
        }
        /// <summary>
        /// To retrieve List of user details based on Id
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<List<JobSeekers>> GetUsersAsync(List<Guid> users)
        {
            return await dbContext.JobSeekerss.Where(u => users.Contains(u.Id)).ToListAsync();
        }
        /// <summary>
        /// To Update details of jobseeker using its Id
        /// </summary>
        /// <param name="jobSeeker"></param>
        /// <returns></returns>
        public async Task<JobSeekers?> UpdateAsync(JobSeekers jobSeeker)
        {
            var existing = await dbContext.JobSeekerss.FirstOrDefaultAsync(x => x.Id == jobSeeker.Id);
            if (existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(jobSeeker);
                await dbContext.SaveChangesAsync();
                return jobSeeker;
            }
            return null;
        }
    }
}