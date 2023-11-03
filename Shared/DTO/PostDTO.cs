using Harmonify.Shared.Enums;
using Harmonify.Shared.Models;

namespace Harmonify.Shared.DTO;

public class PostDTO
{
    public Guid Id { get; set; }
    
    public string? Content { get; set; }
    
    public PostType Type { get; set; }

    public ICollection<PostImage> Images { get; set; }
    
    public DateTime PostedAt { get; set; }
    
    public DateTime? EditedAt { get; set; }
    
    public ICollection<PostLike> Likes { get; set; }
    
    public ApplicationUserDTO Author { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
}