namespace Harmonify.Shared.Models;

public class PostImage
{
    public Guid Id { get; set; }
    
    public string Url { get; set; }
    
    public Guid PostId { get; set; }
    
    public Post Post { get; set; }
}