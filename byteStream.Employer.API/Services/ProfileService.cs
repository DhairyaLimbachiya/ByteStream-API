using byteStream.Employer.API.Services.IServices;
using byteStream.JobSeeker.Api.Models.Dto;
using Newtonsoft.Json;
using System.Text;
namespace byteStream.Employer.API.Services
{
    public class ProfileService(IHttpClientFactory httpClientFactory) : IProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
/// <summary>
/// To get the Details of the Users from the Jobseeker Details
/// </summary>
/// <param name="users"></param>
/// <returns></returns>
        public async Task<List<UserDto>>GetUsers(List<Guid> users)
        {
            var client = _httpClientFactory.CreateClient("Profile");
            var data = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/JobSeeker/getUsers",data);
            var apiContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDto>>(Convert.ToString(apiContent));
        }
    }
}


