using Harmonify.Shared.Models;

namespace Harmonify.Shared.DTO;

public class CommentDTO
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public DateTime PostedAt { get; set; }
    
    public DateTime? EditedAt { get; set; }
    
    public bool IsEditCommentSectionEnabled { get; set; }
    
    public bool IsEditReplySectionEnabled { get; set; }
    
    public bool IsEditReplyToReplySectionEnabled { get; set; }
    
    public bool IsReplySectionEnabled { get; set; }
    
    public bool IsReplyToReplySectionEnabled { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public CommentDTO? ParentComment { get; set; }
    
    public ICollection<CommentDTO> Replies { get; set; }
    
    public ICollection<CommentLikeDTO> Likes { get; set; }
    
    public Guid PostId { get; set; }
    
    public PostDTO Post { get; set; }
    
    public string AuthorId { get; set; }
    
    public ApplicationUserDTO Author { get; set; }
}