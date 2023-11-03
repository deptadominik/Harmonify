using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Harmonify.Client.Services.PostLike;

public class PostLikeService : IPostLikeService
{
    private readonly HttpClient httpClient;

    public PostLikeService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.PostLike?> GetAsync(Guid likeId)
    {
        var response = await httpClient
            .GetAsync($"PostLike?id={likeId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var like = JsonSerializer.Deserialize<Harmonify.Shared.Models.PostLike>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return like;
    }
    
    public async Task<Guid?> CreateAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/PostLike", body);

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
    
    public async Task<bool> DeletePostLikeAsync(Guid postLikeId)
    {
        using var response = await httpClient
            .DeleteAsync($"/PostLike/{postLikeId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}