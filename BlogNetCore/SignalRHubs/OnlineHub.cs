using BlogNetCore.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogNetCore.SignalRHubs
{
    public static class OnlineUser
    {
        public static List<string> Emails = new List<string>();
    }


    [Authorize]
    public class OnlineHub : Hub
    {
        public void CheckOnline()
        {
            var id = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            Clients.All.SendAsync("ShowOnline", id);
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var id = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                OnlineUser.Emails.Add(id);
                Clients.All.SendAsync("ShowOnline", id);
                Clients.All.SendAsync("ShowOnlines", OnlineUser.Emails);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var id = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                OnlineUser.Emails.Remove(id);
                Clients.All.SendAsync("ShowOffline", id);
            }
            
            return base.OnConnectedAsync();
        }
    }
}
