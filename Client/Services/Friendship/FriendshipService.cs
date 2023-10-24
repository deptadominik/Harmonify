using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Enums;
using Harmonify.Shared.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Harmonify.Client.Services;

public class FriendshipService : IFriendshipService
{
    private readonly HttpClient httpClient;

    public FriendshipService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ICollection<Friendship>> GetAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Friendship?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Friendship>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<Friendship>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Friendship>();
    }
    
    public async Task<ICollection<Friendship>> GetPendingFriendshipRequestsAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Friendship/pending?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Friendship>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<Friendship>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Friendship>();
    }
    
    public async Task<Friendship?> GetAsync(string userId, string friendUserId)
    {
        var response = await httpClient
            .GetAsync($"Friendship/status/{userId}/{friendUserId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Friendship?>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? null;
    }
    
    public async Task<ICollection<ApplicationUserDTO>> GetMyFriendsWithAvatarAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Friendship/my-friends/with-avatar?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<ApplicationUserDTO>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<ApplicationUserDTO>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<ApplicationUserDTO>();
    }
    
    public async Task<int> GetNumberOfFriendsAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Friendship/number-of-friends?userId={userId}");

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<int>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<Guid?> CreateFriendshipAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/Friendship", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.Created => (await response.Content
                    .ReadFromJsonAsync<JsonElement>())
                .GetProperty("mainUserId")
                .GetGuid(),
            _ => null
        };
    }
    
    public async Task<Friendship?> UpdateFriendshipAsync(object body)
    {
        using var response = await httpClient.PatchAsJsonAsync(
            "/Friendship/update", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.OK => JsonSerializer.Deserialize<Friendship?>(await response
                    .Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            _ => null
        };
    }
    
    public async Task<bool> DeleteFriendshipAsync(string userId, string friendId)
    {
        using var response = await httpClient
            .DeleteAsync($"/Friendship/{userId}/{friendId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}