using Harmonify.Shared.DTO;

namespace Harmonify.Client.Services.Post;

public interface IPostService
{
    Task<Harmonify.Shared.Models.Post?> GetAsync(Guid postId);
    
    Task<ICollection<PostDTO>> GetMyFeedAsync(string userId);
    
    Task<ICollection<PostDTO>> GetUserPostsAsync(string userId);
    
    Task<Guid?> CreateAsync(object body);

    Task<Harmonify.Shared.Models.Post?> UpdateAsync(object body);
    
    Task<bool> DeleteAsync(Guid postId);
}