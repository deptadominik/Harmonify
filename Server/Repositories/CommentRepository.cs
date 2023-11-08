using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class CommentRepository
{
    private readonly ApplicationDbContext ctx;

    public CommentRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<Comment?> GetAsync(Guid commentId)
    {
        return await ctx.Comments
            .FirstOrDefaultAsync(c => c.Id == commentId);
    }
    
    public async Task<ICollection<Comment>> GetPostCommentsAsync(Guid postId)
    {
        return await ctx.Comments
            .Where(c => c.PostId == postId && c.ParentCommentId == null)
            .OrderBy(c => c.PostedAt)
            .Include(c => c.Author).ThenInclude(u => u.Avatar)
            .Include(c => c.Replies).ThenInclude(r => r.Author).ThenInclude(a => a.Avatar)
            .Include(c => c.Replies).ThenInclude(r => r.Replies).ThenInclude(a => a.Author).ThenInclude(a => a.Avatar)
            .Include(c => c.Likes)
            .Include(c => c.Replies).ThenInclude(r => r.Likes)
            .Include(c => c.Replies).ThenInclude(r => r.Replies).ThenInclude(a => a.Likes)
            .ToArrayAsync();
    }
    
    public async Task<bool> AddAsync(Comment comment)
    {
        await ctx.Comments.AddAsync(comment);
        
        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> RemoveAsync(Guid commentId)
    {
        var result = await ctx.Comments
            .Include(comment => comment.Replies).ThenInclude(comment => comment.Replies)
            .SingleOrDefaultAsync(c => c.Id == commentId);

        if (result == null)
            return false;
        
        var replies = result.Replies;

        foreach (var reply in replies)
        {
            foreach (var repliedReply in reply.Replies)
                ctx.Comments.Remove(repliedReply);
            
            ctx.Comments.Remove(reply);
        }
        
        ctx.Comments.Remove(result);
        await ctx.SaveChangesAsync();
            
        return true;
    }
    
    public async Task<bool> UpdateAsync(Comment comment)
    {
        var entity = await ctx.Comments
            .FirstOrDefaultAsync(c => c.Id == comment.Id);

        if (entity == null)
            return false;

        entity.Content = comment.Content;
        entity.EditedAt = comment.EditedAt;
        
        return await ctx.SaveChangesAsync() > 0;
    }
}