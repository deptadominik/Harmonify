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

    public async Task<AvatarImage?> GetAvatarImage(string userId)
    {
        return await ctx.Avatar
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }
    
    public async Task<bool> AddAvatarImage(AvatarImage avatar)
    {
        await ctx.Avatar.AddAsync(avatar);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteAvatarImage(Guid id)
    {
        var result = await ctx.Avatar
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (result == null) return false;
        
        ctx.Avatar.Remove(result);
        await ctx.SaveChangesAsync();
            
        return true;
    }
}