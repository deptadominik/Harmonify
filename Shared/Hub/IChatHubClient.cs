using Harmonify.Shared.DTO;

namespace Harmonify.Shared.Hub;

public interface IChatHubClient
{
    Task MessageReceived(string userId, MessageDTO message);
}