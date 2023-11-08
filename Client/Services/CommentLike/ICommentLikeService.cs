namespace Harmonify.Client.Services.CommentLike;

public interface ICommentLikeService
{
    Task<Harmonify.Shared.Models.CommentLike?> GetAsync(Guid likeId);
    
    Task<Guid?> CreateAsync(object body);
    
    Task<bool> DeleteAsync(Guid commentLikeId);
}