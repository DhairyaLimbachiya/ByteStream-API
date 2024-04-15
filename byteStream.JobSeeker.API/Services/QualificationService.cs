using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace byteStream.JobSeeker.API.Services
{
    public class QualificationService : IQualificationService
    {
        private readonly AppDbContext dbContext;

        public QualificationService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Qualification> CreateAsync(Qualification qualification)
        {
            await dbContext.Qualifications.AddAsync(qualification);
            await dbContext.SaveChangesAsync();
            return qualification;
        }

        public async Task<Qualification?> UpdateAsync(Qualification qualification)
        {
            var existing = await dbContext.Qualifications.FirstOrDefaultAsync(x => x.Id == qualification.Id);
            if (existing != null)
            {
               

                dbContext.Entry(existing).CurrentValues.SetValues(qualification);
                await dbContext.SaveChangesAsync();
                return qualification;
            }
            return null;
        }


public async Task<Qualification?> DeleteAsync(Guid id)
        {
            var existing = await dbContext.Qualifications.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            dbContext.Qualifications.Remove(existing);
            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<Qualification?> GetByIdAsync(Guid id)
        {
            return await dbContext.Qualifications.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<Qualification>?> GetAllAsync(Guid id)
        {
            return await dbContext.Qualifications.Where(x => x.UserID == id).ToListAsync();
        }
    }
}
