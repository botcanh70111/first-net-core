using Microsoft.AspNetCore.SignalR;
using Services.Constants;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNetCore.SignalRHubs
{
    public class AdminNotificationHub : Hub
    {
        public async Task SendNotification(string message, string url)
        {
            var currentEmail = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var groupdUserEmails = Context.User.Claims.FirstOrDefault(x => x.Type == BlogClaimTypes.GroupEmails).Value;
            await Clients.Users(groupdUserEmails.Split(',')).SendAsync("ReceiveNotification", currentEmail, message, url);
        }
    }
}
