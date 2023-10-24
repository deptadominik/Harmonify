using Harmonify.Shared.Enums;

namespace Harmonify.Shared.Models;

public class Notification
{
    public Guid Id { get; set; }
    
    public NotificationType Type { get; set; }
    
    public string Description { get; set; }
    
    public string ReferenceUrl { get; set; }
    
    public DateTime ReceivedAt { get; set; }
    
    public bool MarkedAsSeen { get; set; }
    
    public string UserId { get; set; }
    
    public ApplicationUser User { get; set; }
}