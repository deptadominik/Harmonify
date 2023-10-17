using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class FriendshipRepository
{
    private readonly ApplicationDbContext ctx;

    public FriendshipRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<ICollection<Friendship>> GetFriendshipsAsync(string userId)
    {
        return await ctx.Friendships
            .Where(x => x.MainUserId == userId || x.FriendUserId == userId)
            .ToListAsync();
    }

    public async Task<Friendship?> GetFriendshipAsync(string mainUserId, string friendUserId)
    {
        var friendship = await ctx.Friendships
            .Where(x => x.MainUserId == mainUserId && x.FriendUserId == friendUserId)
            .FirstOrDefaultAsync();

        if (friendship == null)
        {
            return await ctx.Friendships
                .Where(x => x.MainUserId == friendUserId && x.FriendUserId == mainUserId)
                .FirstOrDefaultAsync();
        }

        return friendship;
    }

    public ICollection<ApplicationUser> GetMyFriendsWithAvatar(string userId)
    {
        var myFriends = ctx.Friendships
            .Include(x => x.MainUser).ThenInclude(x => x.Avatar)
            .Include(x => x.MainUser).ThenInclude(x => x.Address)
            .Include(x => x.FriendUser).ThenInclude(x => x.Avatar)
            .Include(x => x.FriendUser).ThenInclude(x => x.Address)
            .Where(x => x.MainUserId == userId)
            .Select(x => x.FriendUser)
            .ToList();

        return myFriends.Concat(ctx.Friendships
                .Include(x => x.MainUser).ThenInclude(x => x.Avatar)
                .Include(x => x.FriendUser).ThenInclude(x => x.Avatar)
                .Where(x => x.FriendUserId == userId)
                .Select(x => x.MainUser)
                .ToList())
            .ToList();
    }
    
    public int GetNumberOfFriends(string userId)
    {
        var myFriends = ctx.Friendships
            .Where(x => x.MainUserId == userId)
            .Select(x => x.FriendUser)
            .ToList();

        return myFriends.Concat(ctx.Friendships
                .Where(x => x.FriendUserId == userId)
                .Select(x => x.MainUser)
                .ToList())
            .Count();
    }

    public async Task<bool> AddFriendshipAsync(Friendship friendship)
    {
        await ctx.Friendships.AddAsync(friendship);

        return await ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<bool> DeleteFriendshipAsync(string userId, string friendId)
    {
        var result = await ctx.Friendships
            .SingleOrDefaultAsync(e => e.MainUserId == userId && e.FriendUserId == friendId)
                     ?? await ctx.Friendships
            .SingleOrDefaultAsync(e => e.MainUserId == friendId && e.FriendUserId == userId);

        if (result == null)
            return false;
        
        ctx.Friendships.Remove(result);
        await ctx.SaveChangesAsync();
            
        return true;
    }
}