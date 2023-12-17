namespace Harmonify.Shared.DTO;

public class MessageDTO
{
    public Guid Id { get; set; }

    public string FromUserId { get; set; }
    
    public string ToUserId { get; set; }

    public string Content { get; set; }

    public DateTime SentOn { get; set; }

    public virtual ApplicationUserDTO FromUser { get; set; }

    public virtual ApplicationUserDTO ToUser { get; set; }
}