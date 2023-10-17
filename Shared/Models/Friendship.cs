using Harmonify.Shared.Enums;

namespace Harmonify.Shared.Models;

public class Friendship
{
    public string MainUserId { get; set; }

    public virtual ApplicationUser MainUser { get; set; }

    public string FriendUserId { get; set; }

    public virtual ApplicationUser FriendUser { get; set; }
    
    public FriendshipStatus Status { get; set; }
}