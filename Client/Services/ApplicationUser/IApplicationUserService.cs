using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public interface IApplicationUserService
{
    Task<ApplicationUserDTO?> GetUserByIdAsync(string userId);
    
    Task<ApplicationUserDTO?> GetUserByEmailAsync(string email);

    Task<ICollection<ApplicationUserDTO>> GetUsersByPhraseAsync(string phrase);
}