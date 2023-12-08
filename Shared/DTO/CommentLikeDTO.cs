namespace Harmonify.Shared.DTO;

public class CommentLikeDTO
{
    public Guid Id { get; set; }
    
    public Guid CommentId { get; set; }
    
    public CommentDTO Comment { get; set; }

    public string UserId { get; set; }
    
    public ApplicationUserDTO User { get; set; }
}