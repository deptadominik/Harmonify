namespace Harmonify.Client.Services.Notification;

public interface INotificationService
{
    Task<Harmonify.Shared.Models.Notification?> GetAsync(Guid notificationId);
    
    Task<Guid?> CreateAsync(object body);
    
    Task<ICollection<Harmonify.Shared.Models.Notification>> GetMyNotificationsAsync(string userId);
    
    Task<Harmonify.Shared.Models.Notification?> MarkAsSeenAsync(object body);

    Task<bool> MarkAllAsSeenAsync(object body);
}