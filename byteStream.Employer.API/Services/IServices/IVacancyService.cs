using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Models;


namespace byteStream.Employer.API.Services.IServices
{
	public interface IVacancyService
	{
		Task<Vacancy> CreateAsync(Vacancy vacancy);
        Task<Vacancy?> GetByIdAsync(Guid id);
		Task<List<Vacancy?>> GetAllAsync();
        Task<List<Vacancy?>> GetByCompanyAsync(Guid id);
		Task<Vacancy?> UpdateAsync( Vacancy vacancy);
		Task<Vacancy?> DeleteAsync(Guid id);
        Task<Boolean> CheckApplicationAsync(Guid userId, Guid vacancyId);
    }
}
