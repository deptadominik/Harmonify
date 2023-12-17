using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class MessageRepository
{
    private readonly ApplicationDbContext ctx;

    public MessageRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<Message?> GetAsync(Guid messageId)
    {
        return await ctx.Messages
            .Include(u => u.FromUser)
            .Include(u => u.ToUser)
            .FirstOrDefaultAsync(n => n.Id == messageId);
    }
    
    public async Task<ICollection<Message>> GetMessagesAsync(string userId, string otherUserId)
    {
        return await ctx.Messages
            .Where(m => (m.FromUserId == otherUserId && m.ToUserId == userId)
                        || (m.ToUserId == otherUserId && m.FromUserId == userId))
            .OrderBy(m => m.SentOn)
            .Include(u => u.FromUser)
            .Include(u => u.ToUser)
            .ToArrayAsync();
    }
    
    public async Task<ICollection<string>> GetMyChatUsersAsync(string userId)
    {
        var users =  await ctx.Messages
            .Where(m => m.FromUserId == userId || m.ToUserId == userId)
            .Select(m => new { From = m.FromUserId, To = m.ToUserId })
            .Distinct()
            .ToListAsync();
        
        var uniqueUserIds = new List<string>();
        
        users.ForEach(u =>
        {
            if (u.From != userId)
                uniqueUserIds.Add(u.From);
            if (u.To != userId)
                uniqueUserIds.Add(u.To);
        });
        
        return uniqueUserIds;
    }
    
    public async Task<bool> AddAsync(Message message)
    {
        await ctx.Messages.AddAsync(message);
        
        return await ctx.SaveChangesAsync() > 0;
    }
}