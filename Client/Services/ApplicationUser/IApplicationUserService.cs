using Harmonify.Shared.DTO;

namespace Harmonify.Client.Services.ApplicationUser;

public interface IApplicationUserService
{
    Task<ApplicationUserDTO?> GetUserByIdAsync(string userId);
    
    Task<ApplicationUserDTO?> GetUserByEmailAsync(string email);

    Task<ICollection<ApplicationUserDTO>> GetUsersByPhraseAsync(string phrase);
}