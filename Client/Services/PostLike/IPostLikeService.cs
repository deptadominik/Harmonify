namespace Harmonify.Client.Services.PostLike;

public interface IPostLikeService
{
    Task<Harmonify.Shared.Models.PostLike?> GetAsync(Guid likeId);
    
    Task<Guid?> CreateAsync(object body);
    
    Task<bool> DeletePostLikeAsync(Guid postLikeId);
}