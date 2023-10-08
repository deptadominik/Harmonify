namespace Harmonify.Shared.DTO;

public class AvatarImageDTO
{
    public Guid Id { get; set; }
    
    public string FileName { get; set; }
    
    public byte[] Content { get; set; }
    
    public string UserId { get; set; }
}