using byteStream.Auth.Api.Data;
using byteStream.Auth.Api.Models;
using byteStream.Auth.Api.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace byteStream.Auth.Api.Services
{
    public class AuthService:IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService tokenservice;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, ITokenService tokenservice)
        {
            _db = db;
            _userManager = userManager;
            this.tokenservice = tokenservice;
        }

        public async Task<string?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = tokenservice.CreateJWTToken(user, roles.ToList());

                    return jwtToken.ToString();
                }

            }
            return null;
        }

        public async Task<ResponseDto?> RegisterAsync(ApplicationUser user, string password, string roleName)
        {
            var res = new ResponseDto();
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.Email == user.Email);
                    await _userManager.AddToRoleAsync(userToReturn, roleName);
                    res.Result = userToReturn;
                    return res;

                }
                else
                {
                    res.IsSuccess = false;
                    res.Message = result.Errors.FirstOrDefault().Description;
                    return res;
                }

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message.ToString();
                return res;

            }
            return res;
        }
    }
}