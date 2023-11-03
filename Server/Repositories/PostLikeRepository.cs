using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class PostLikeRepository
{
    private readonly ApplicationDbContext ctx;

    public PostLikeRepository(ApplicationDbContext context)
    {
        ctx = context;
    }
    
    public async Task<PostLike?> GetAsync(Guid likeId)
    {
        return await ctx.PostLikes
            .FirstOrDefaultAsync(pl => pl.Id == likeId);
    }
    
    public async Task<bool> AddAsync(PostLike like)
    {
        await ctx.PostLikes.AddAsync(like);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> RemoveAsync(Guid postLikeId)
    {
        var entity = await ctx.PostLikes
            .SingleAsync(x => x.Id == postLikeId);
        ctx.PostLikes.Remove(entity);
        
        return await ctx.SaveChangesAsync() > 0;
    }
}