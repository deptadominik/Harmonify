namespace Harmonify.Shared.Models;

public class AvatarImage
{
    public Guid Id { get; set; }
    
    public string FileName { get; set; }
    
    public byte[] Content { get; set; }
    
    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
}