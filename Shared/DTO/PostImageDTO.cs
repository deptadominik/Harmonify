namespace Harmonify.Shared.DTO;

public class PostImageDTO
{
    public Guid Id { get; set; }
    
    public string Url { get; set; }
    
    public string Name { get; set; }
    
    public Guid PostId { get; set; }
}