using Harmonify.Shared.Enums;

namespace Harmonify.Shared.Models;

public class Post
{
    public Guid Id { get; set; }
    
    public string? Content { get; set; }
    
    public PostType Type { get; set; }

    public ICollection<PostImage> Images { get; set; }
    
    public DateTime PostedAt { get; set; }
    
    public DateTime? EditedAt { get; set; }
    
    public ICollection<PostLike> Likes { get; set; }
    
    public string AuthorId { get; set; }
    
    public ApplicationUser Author { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
}