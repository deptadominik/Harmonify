using Harmonify.Server.Data;
using Harmonify.Shared.Enums;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class PostRepository
{
    private readonly ApplicationDbContext ctx;

    public PostRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<Post?> GetAsync(Guid postId)
    {
        return await ctx.Posts
            .FirstOrDefaultAsync(n => n.Id == postId);
    }
    
    public async Task<ICollection<Post>> GetMyFeedAsync(string userId)
    {
        var myFriends = ctx.Friendships
            .Where(x => x.MainUserId == userId && x.Status == FriendshipStatus.Accepted)
            .Select(x => x.FriendUserId)
            .ToList();
        
        myFriends.AddRange(ctx.Friendships
            .Where(x => x.FriendUserId == userId && x.Status == FriendshipStatus.Accepted)
            .Select(x => x.MainUserId));
        
        return await ctx.Posts
            .Where(p => myFriends.Contains(p.AuthorId) || p.AuthorId == userId)
            .Include(p => p.Author).ThenInclude(a => a.Avatar)
            .Include(p => p.Likes)
            .ThenInclude(pl => pl.User)
            .Include(p => p.Images)
            .OrderByDescending(p => p.PostedAt)
            .ToListAsync();
    }
    
    public async Task<ICollection<Post>> GetUserPostsAsync(string userId)
    {
        return await ctx.Posts
            .Where(p => p.AuthorId == userId)
            .Include(p => p.Images)
            .Include(p => p.Author).ThenInclude(a => a.Avatar)
            .Include(p => p.Likes).ThenInclude(pl => pl.User)
            .OrderByDescending(p => p.PostedAt)
            .ToListAsync();
    }
    
    public async Task<bool> AddAsync(Post post)
    {
        await ctx.Posts.AddAsync(post);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> RemoveAsync(Guid postId)
    {
        var entity = await ctx.Posts
            .SingleAsync(x => x.Id == postId);
        ctx.Posts.Remove(entity);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> UpdateContentAsync(Post post)
    {
        var entity = await ctx.Posts
            .FirstOrDefaultAsync(n => n.Id == post.Id);

        if (entity == null)
            return false;

        entity.Content = post.Content;
        entity.EditedAt = post.EditedAt;
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> UpdateCommentsCountAsync(Post post)
    {
        var entity = await ctx.Posts
            .FirstOrDefaultAsync(n => n.Id == post.Id);

        if (entity == null)
            return false;

        entity.CommentsCount = post.CommentsCount;
        
        return await ctx.SaveChangesAsync() > 0;
    }
}