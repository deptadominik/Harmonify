using System.Net.Http.Json;
using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly HttpClient httpClient;

    public ApplicationUserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    public async Task<ApplicationUser?> GetUserByEmail(string email)
    {
        return await httpClient.GetFromJsonAsync<ApplicationUser>(
            $"ApplicationUser?email={email}");
    }

}