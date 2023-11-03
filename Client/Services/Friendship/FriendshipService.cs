using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Harmonify.Shared.DTO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Harmonify.Client.Services.Friendship;

public class FriendshipService : IFriendshipService
{
    private readonly HttpClient httpClient;

    public FriendshipService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ICollection<Harmonify.Shared.Models.Friendship>> GetAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Friendship?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Harmonify.Shared.Models.Friendship>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<Harmonify.Shared.Models.Friendship>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Harmonify.Shared.Models.Friendship>();
    }
    
    public async Task<ICollection<Harmonify.Shared.Models.Friendship>> GetPendingFriendshipRequestsAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Friendship/pending?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Harmonify.Shared.Models.Friendship>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<Harmonify.Shared.Models.Friendship>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Harmonify.Shared.Models.Friendship>();
    }
    
    public async Task<Harmonify.Shared.Models.Friendship?> GetAsync(string userId, string friendUserId)
    {
        var response = await httpClient
            .GetAsync($"Friendship/status/{userId}/{friendUserId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Harmonify.Shared.Models.Friendship?>(content,
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
    
    public async Task<Harmonify.Shared.Models.Friendship?> UpdateFriendshipAsync(object body)
    {
        using var response = await httpClient.PatchAsJsonAsync(
            "/Friendship/update", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.OK => JsonSerializer.Deserialize<Harmonify.Shared.Models.Friendship?>(await response
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