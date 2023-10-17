using Harmonify.Server.Data;
using Harmonify.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Harmonify.Server.Repositories;

public class ApplicationUserRepository
{
    private readonly ApplicationDbContext ctx;

    public ApplicationUserRepository(ApplicationDbContext context)
    {
        ctx = context;
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await ctx.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
    
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await ctx.Users
            .Include(a => a.Address)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<ApplicationUser?> GetUserWithFriendsByEmailAsync(string email)
    {
        return await ctx.Users
            .Include(x => x.Friends)
            .Include(a => a.Address)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<ICollection<ApplicationUser>> GetUsersByPartialNameAsync(string phrase)
    {
        return await ctx.Users
            .Where(u => u.FirstName.Contains(phrase) || u.LastName.Contains(phrase)
                || (u.FirstName + " " + u.LastName).Contains(phrase))
            .Include(a => a.Avatar)
            .Include(a => a.Address)
            .ToListAsync();
    }
}