namespace Harmonify.Shared.Models;

public class Message
{
    public Guid Id { get; set; }

    public string FromUserId { get; set; }
    
    public string ToUserId { get; set; }

    public string Content { get; set; }

    public DateTime SentOn { get; set; }

    public virtual ApplicationUser FromUser { get; set; }

    public virtual ApplicationUser ToUser { get; set; }
}