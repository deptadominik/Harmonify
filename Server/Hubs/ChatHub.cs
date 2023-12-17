using Harmonify.Shared.Hub;
using Microsoft.AspNetCore.SignalR;

namespace Harmonify.Server.Hubs;

public class ChatHub : Hub<IChatHubClient>, IChatHubServer
{
    public ChatHub()
    {
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
