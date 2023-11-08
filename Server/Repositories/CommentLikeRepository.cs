using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class CommentLikeRepository
{
    private readonly ApplicationDbContext ctx;

    public CommentLikeRepository(ApplicationDbContext context)
    {
        ctx = context;
    }
    
    public async Task<CommentLike?> GetAsync(Guid likeId)
    {
        return await ctx.CommentLikes
            .FirstOrDefaultAsync(cl => cl.Id == likeId);
    }
    
    public async Task<bool> AddAsync(CommentLike like)
    {
        await ctx.CommentLikes.AddAsync(like);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> RemoveAsync(Guid commentLikeId)
    {
        var entity = await ctx.CommentLikes
            .SingleAsync(x => x.Id == commentLikeId);
        ctx.CommentLikes.Remove(entity);
        
        return await ctx.SaveChangesAsync() > 0;
    }
}