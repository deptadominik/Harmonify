using Harmonify.Shared.DTO;

namespace Harmonify.Client.Services.CommentService;

public interface ICommentService
{
    Task<Harmonify.Shared.Models.Comment?> GetAsync(Guid commentId);
    
    Task<ICollection<CommentDTO>> GetPostCommentsAsync(Guid postId);
    
    Task<Guid?> CreateAsync(object body);
    
    Task<CommentDTO?> UpdateAsync(object body);
    
    Task<bool> DeleteAsync(Guid commentId);
}