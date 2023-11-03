using Harmonify.Shared.DTO;

namespace Harmonify.Client.Services.Friendship;

public interface IFriendshipService
{
    Task<Harmonify.Shared.Models.Friendship?> GetAsync(string userId, string friendUserId);

    Task<ICollection<Harmonify.Shared.Models.Friendship>> GetPendingFriendshipRequestsAsync(string userId);
    
    Task<ICollection<Harmonify.Shared.Models.Friendship>> GetAsync(string userId);

    Task<ICollection<ApplicationUserDTO>> GetMyFriendsWithAvatarAsync(string userId);
    
    Task<int> GetNumberOfFriendsAsync(string userId);
    
    Task<Guid?> CreateFriendshipAsync(object body);

    Task<Harmonify.Shared.Models.Friendship?> UpdateFriendshipAsync(object body);
    
    Task<bool> DeleteFriendshipAsync(string userId, string friendId);
}