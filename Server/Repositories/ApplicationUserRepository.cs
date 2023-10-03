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

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await ctx.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}