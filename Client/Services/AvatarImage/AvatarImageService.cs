using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Harmonify.Shared.Models;

namespace Harmonify.Client.Services;

public class AvatarImageService : IAvatarImageService
{
    private readonly HttpClient httpClient;

    public AvatarImageService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<AvatarImage?> GetAvatarAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"AvatarImage?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var avatar = JsonSerializer.Deserialize<AvatarImage>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return avatar;
    }

    public async Task<Guid?> AddAvatarAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/AvatarImage", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.Created => (await response.Content
                    .ReadFromJsonAsync<JsonElement>())
                .GetProperty("id")
                .GetGuid(),
            _ => null
        };
    }
    
    public async Task<bool> DeleteAvatarAsync(Guid avatarId)
    {
        using var response = await httpClient.DeleteAsync($"/AvatarImage/{avatarId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}