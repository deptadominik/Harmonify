using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class PostImageRepository
{
    private readonly ApplicationDbContext ctx;

    public PostImageRepository(ApplicationDbContext context)
    {
        ctx = context;
    }
    
    public async Task<PostImage?> GetAsync(Guid imageId)
    {
        return await ctx.PostImages
            .FirstOrDefaultAsync(pl => pl.Id == imageId);
    }
    
    public async Task<bool> AddAsync(PostImage image)
    {
        await ctx.PostImages.AddAsync(image);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> AddAsync(IEnumerable<PostImage> images)
    {
        await ctx.PostImages.AddRangeAsync(images);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> RemoveAsync(Guid imageId)
    {
        var entity = await ctx.PostImages
            .SingleAsync(x => x.Id == imageId);
        ctx.PostImages.Remove(entity);
        
        return await ctx.SaveChangesAsync() > 0;
    }
}