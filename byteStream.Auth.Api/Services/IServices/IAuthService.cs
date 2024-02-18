using byteStream.Auth.Api.Models;

namespace byteStream.Auth.Api.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto?> RegisterAsync(ApplicationUser applicationUser, string password, string roleName);
        Task<string?> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
