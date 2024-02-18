using byteStream.Auth.Api.Models;

namespace byteStream.Auth.Api.Services.IServices
{
    public interface ITokenService
    {
        string CreateJWTToken(ApplicationUser user, List<string> roles);

    }
}

