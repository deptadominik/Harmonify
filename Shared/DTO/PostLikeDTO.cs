namespace Harmonify.Shared.DTO;

public class PostLikeDTO
{
    public Guid Id { get; set; }

    public Guid PostId { get; set; }

    public PostDTO Post { get; set; }

    public string UserId { get; set; }

    public ApplicationUserDTO User { get; set; }
}