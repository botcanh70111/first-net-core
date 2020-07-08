using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNetCore.SignalRHubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string toEmail, string message)
        {
            //var avatar = "/images/zeen-chin- (9).jpg";
            var email = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            await Clients.User(toEmail).SendAsync("ReceiveMessage", email, toEmail, message);
        }
    }
}
