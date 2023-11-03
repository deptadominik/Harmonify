namespace Harmonify.Shared.Models;

public class CommentLike
{
    public Guid Id { get; set; }
    
    public Guid CommentId { get; set; }
    
    public Comment Comment { get; set; }

    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
}