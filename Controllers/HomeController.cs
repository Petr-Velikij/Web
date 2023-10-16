using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using WebTutorCore;

namespace WebApp.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHubContext<MessageHub> messageHub;
        public HomeController([NotNull] IHubContext<MessageHub> messageHub) {
            this.messageHub = messageHub;
        }
        public async Task<IActionResult> Create(MessagePost messagePost)
        {
            await messageHub.Clients.All.SendAsync("sendToReact", "The message" + messagePost.Message + " [end]");
            return Ok();
        }
    }

    public class MessagePost
    {
        public virtual string Message { get; set; }
    }
}