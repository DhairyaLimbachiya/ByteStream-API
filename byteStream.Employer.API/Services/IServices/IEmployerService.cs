using byteStream.Employer.Api.Models;

using System.Drawing;
namespace byteStream.Employer.API.Services.IServices
{
	public interface IEmployerService
	{
		Task<Employeer?> GetByIdAsync(Guid id);
		Task<Employeer?> GetByCompanyName(string companyName);
		Task<Employeer> CreateAsync(Employeer employer);
		Task<Employeer?> UpdateAsync( Employeer employer);
		Task<Employeer?> DeleteAsync(Guid id);
		Task<string> GetOrganizationName(Guid id);

    }
}
