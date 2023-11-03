namespace Harmonify.Client.Services.AvatarImage;

public interface IAvatarImageService
{
    Task<Harmonify.Shared.Models.AvatarImage?> GetAvatarAsync(string userId);
    
    Task<Guid?> AddAvatarAsync(object body);
    
    Task<bool> DeleteAvatarAsync(Guid avatarId);
}