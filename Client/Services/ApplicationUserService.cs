using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly HttpClient httpClient;

    public ApplicationUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await httpClient.GetFromJsonAsync<ApplicationUser>(
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
    
    public async Task<ApplicationUser?> GetUserWithFriendsByEmailAsync(string email)
    {
        return await httpClient.GetFromJsonAsync<ApplicationUser>(
            $"ApplicationUser/with-friends?email={email}");
    }
}