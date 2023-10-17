using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public interface IFriendshipService
{
    Task<ICollection<Friendship>> GetAsync(string userId);

    Task<ICollection<ApplicationUserDTO>> GetMyFriendsWithAvatarAsync(string userId);
    
    Task<int> GetNumberOfFriendsAsync(string userId);
    
    Task<Guid?> CreateFriendshipAsync(object body);
    
    Task<bool> DeleteFriendshipAsync(string userId, string friendId);
}