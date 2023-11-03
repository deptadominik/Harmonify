namespace Harmonify.Shared.Models;

public class Comment
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public DateTime PostedAt { get; set; }
    
    public DateTime EditedAt { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public Comment? ParentComment { get; set; }
    
    public ICollection<Comment> Replies { get; set; }
    
    public ICollection<CommentLike> Likes { get; set; }
    
    public Guid PostId { get; set; }
    
    public Post Post { get; set; }
    
    public string AuthorId { get; set; }
    
    public ApplicationUser Author { get; set; }
}