using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Claims;

namespace ByteStream.Employer.Api.Repository
{
    public class EmployerService : IEmployerService
	{
		private readonly AppDbContext dbContext;

		public EmployerService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}


		public async Task<Employeer> CreateAsync(Employeer employer)
		{
			await dbContext.Employers.AddAsync(employer);
			await dbContext.SaveChangesAsync();
			return employer;
		}


		public async Task<Employeer?> GetByIdAsync(Guid id)
		{
			return await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Employeer?> UpdateAsync( Employeer employer)
		{
		
			dbContext.Employers.Update(employer);

		

			await dbContext.SaveChangesAsync();

			return employer;
		}

		public async Task<Employeer?> DeleteAsync(Guid id)
		{
			var existing= await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return null;

			dbContext.Employers.Remove(existing);
			await dbContext.SaveChangesAsync();
			return existing;

		}
		public async Task <string> GetOrganizationName(Guid Id)
		{
            var user =await dbContext.Employers.FirstOrDefaultAsync(x=>x.Id==Id) ;
			var OrganizationName = user.Organization;
			return OrganizationName;
        }

        public async Task<Employeer?> GetByCompanyName(string companyName)
        {
			var company = await dbContext.Employers.FirstOrDefaultAsync(u => u.Organization == companyName);
			return company;
        }
    }
}

