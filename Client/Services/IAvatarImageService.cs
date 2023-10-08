using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public interface IAvatarImageService
{
    Task<AvatarImage?> GetAvatarAsync(string userId);
    
    Task<Guid?> AddAvatarAsync(object body);
    
    Task<bool> DeleteAvatarAsync(Guid avatarId);
}