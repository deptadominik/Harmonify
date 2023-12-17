using Harmonify.Server.Data;
using Harmonify.Shared.Enums;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class NotificationRepository
{
    private readonly ApplicationDbContext ctx;

    public NotificationRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<Notification?> GetAsync(Guid notificationId)
    {
        return await ctx.Notifications
            .Include(u => u.User)
            .FirstOrDefaultAsync(n => n.Id == notificationId);
    }
    
    public async Task<bool> AddAsync(Notification notification)
    {
        await ctx.Notifications.AddAsync(notification);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public ICollection<Notification> GetUsersNotifications(string userId)
    {
        return ctx.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(x => x.ReceivedAt)
            .ToArray();
    }
    
    public async Task<bool> UpdateMarkedAsSeenAsync(Notification notification)
    {
        var entity = await ctx.Notifications
            .FirstOrDefaultAsync(n => n.Id == notification.Id);

        if (entity == null)
            return false;

        entity.MarkedAsSeen = notification.MarkedAsSeen;
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> MarkAllNotificationsAsSeen(string userId)
    {
        var entities = ctx.Notifications
            .Where(n => n.UserId == userId && n.MarkedAsSeen == false);

        foreach (var entity in entities)
            entity.MarkedAsSeen = true;
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteAllMessageNotifications(string userId)
    {
        var entities = ctx.Notifications
            .Where(n => n.UserId == userId && n.MarkedAsSeen == false && n.Type == NotificationType.Message);

         ctx.Notifications.RemoveRange(entities);
        
        return await ctx.SaveChangesAsync() > 0;
    }
}