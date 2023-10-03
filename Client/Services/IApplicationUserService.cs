using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public interface IApplicationUserService
{
    Task<ApplicationUser?> GetUserByEmail(string email);
}