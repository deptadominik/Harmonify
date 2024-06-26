using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class AvatarImageRepository
{
    private readonly ApplicationDbContext ctx;

    public AvatarImageRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<AvatarImage?> GetAvatarImageAsync(string userId)
    {
        return await ctx.Avatars
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }
    
    public async Task<bool> AddAvatarImageAsync(AvatarImage avatar)
    {
        await ctx.Avatars.AddAsync(avatar);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteAvatarImageAsync(Guid id)
    {
        var result = await ctx.Avatars
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (result == null) return false;
        
        ctx.Avatars.Remove(result);
        await ctx.SaveChangesAsync();
            
        return true;
    }
}