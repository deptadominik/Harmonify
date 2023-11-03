namespace Harmonify.Client.Services.Post;

public interface IPostService
{
    Task<Harmonify.Shared.Models.Post?> GetAsync(Guid postId);
    
    Task<ICollection<Harmonify.Shared.Models.Post>> GetMyFeedAsync(string userId);
    
    Task<ICollection<Harmonify.Shared.Models.Post>> GetUserPostsAsync(string userId);
    
    Task<Guid?> CreateAsync(object body);

    Task<Harmonify.Shared.Models.Post?> UpdateAsync(object body);
    
    Task<bool> DeleteAsync(Guid postId);
}