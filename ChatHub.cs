using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebTutor
{
    
    [EnableCors("Test")]
    public class ChatHub : Hub
    {
        [Authorize]
        public async Task Send(string message, string to)
        {
            if (message == "" || to == "") return;
            if (Context.UserIdentifier is string userName)
            {
                Console.WriteLine($"From:{Context?.User?.FindFirst("id")?.Value} To:{to} Message:{message}");
                await Clients.Users(to, userName).SendAsync("Receive", message, Context?.User?.Identity?.Name);
            }
            else
            {
                Console.WriteLine($"From:{Context?.User?.FindFirst("id")?.Value} To:{to} Message:{message} ERROR");
            }
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connect: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection?.User?.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
        }
    }
}
