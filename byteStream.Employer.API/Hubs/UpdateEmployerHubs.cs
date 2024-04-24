using byteStream.Employer.API.Models.Dto;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace byteStream.Employer.API.Hubs
{
    public class UpdateEmployerHubs : Hub
    {

        public static List<string> GroupsJoined { get; set; } = new List<string>();

        public async Task InitializeConnection(EmployerDto employerDto) 
        {
            if (!GroupsJoined.Contains(Context.ConnectionId + ":" + employerDto.Organization))
            {
                GroupsJoined.Add(Context.ConnectionId + ":" + employerDto.Organization);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, employerDto.Organization);

            // Send the employerDto object to all clients in the group except the caller
        }
        public async Task SendData(EmployerDto employerDto)
        {
            await Clients.OthersInGroup(employerDto.Organization).SendAsync("UpdateEmployerMethod", employerDto);

        }
       
    }
}