using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Harmonify.Shared.DTO;

namespace Harmonify.Client.Services.ApplicationUser;

public class ApplicationUserService : IApplicationUserService
{
    private readonly HttpClient httpClient;

    public ApplicationUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<ApplicationUserDTO?> GetUserByIdAsync(string userId)
    {
        return await httpClient.GetFromJsonAsync<ApplicationUserDTO>(
            $"ApplicationUser?userId={userId}");
    }
    
    public async Task<ApplicationUserDTO?> GetUserByEmailAsync(string email)
    {
        return await httpClient.GetFromJsonAsync<ApplicationUserDTO>(
            $"ApplicationUser/by-email?email={email}");
    }
    
    public async Task<ICollection<ApplicationUserDTO>> GetUsersByPhraseAsync(string phrase)
    {
        var response = await httpClient.GetAsync(
            $"ApplicationUser/by-phrase?phrase={phrase}");
        
        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<ApplicationUserDTO>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<ApplicationUserDTO>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<ApplicationUserDTO>();
    }
    
    public async Task<Harmonify.Shared.Models.ApplicationUser?> GetUserWithFriendsByEmailAsync(string email)
    {
        return await httpClient.GetFromJsonAsync<Harmonify.Shared.Models.ApplicationUser>(
            $"ApplicationUser/with-friends?email={email}");
    }
}