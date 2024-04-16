using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Claims;

namespace byteStream.Employer.Api.Services
{
    public class EmployerService : IEmployerService
	{
		private readonly AppDbContext dbContext;

		public EmployerService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// To add a new Employer profile to the Database
		/// </summary>
		/// <param name="employer"></param>
		/// <returns></returns>
		public async Task<Employeer> CreateAsync(Employeer employer)
		{
			await dbContext.Employers.AddAsync(employer);
			await dbContext.SaveChangesAsync();
			return employer;
		}

		/// <summary>
		/// To fetch the details of an Employer through its ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Employeer?> GetByIdAsync(Guid id)
		{
			return await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == id);
		}
		/// <summary>
		/// To update the details of the Employer
		/// </summary>
		/// <param name="employer"></param>
		/// <returns></returns>

	
		public async Task<Employeer?> UpdateAsync( Employeer employer)
		{

			var existingCompany = await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == employer.Id);

			if (existingCompany != null)
			{
				dbContext.Entry(existingCompany).CurrentValues.SetValues(employer);
				await dbContext.SaveChangesAsync();
				return employer;
			}
			return null;

		
		}

		/// <summary>
		/// To Delete the details of an employer using its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Employeer?> DeleteAsync(Guid id)
		{
			var existing= await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return null;

			dbContext.Employers.Remove(existing);
			await dbContext.SaveChangesAsync();
			return existing;

		}
		/// <summary>
		/// To get the Oranization name the employee is connected to
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task <string> GetOrganizationName(Guid Id)
		{
            var user =await dbContext.Employers.FirstOrDefaultAsync(x=>x.Id==Id) ;
			var OrganizationName = user.Organization;
			return OrganizationName;
        }
		/// <summary>
		/// To get details of the Employee By Company Name
		/// </summary>
		/// <param name="companyName"></param>
		/// <returns></returns>
        public async Task<Employeer?> GetByCompanyName(string companyName)
        {
			var company = await dbContext.Employers.FirstOrDefaultAsync(u => u.Organization == companyName);
			return company;
        }
    }
}

