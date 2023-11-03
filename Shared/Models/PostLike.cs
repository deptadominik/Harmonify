namespace Harmonify.Shared.Models;

public class PostLike
{
    public Guid Id { get; set; }
    
    public Guid PostId { get; set; }
    
    public Post Post { get; set; }

    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
}