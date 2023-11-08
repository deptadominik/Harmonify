using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Harmonify.Client.Services.CommentLike;

public class CommentLikeService : ICommentLikeService
{
    private readonly HttpClient httpClient;

    public CommentLikeService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.CommentLike?> GetAsync(Guid likeId)
    {
        var response = await httpClient
            .GetAsync($"CommentLike?id={likeId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var like = JsonSerializer.Deserialize<Harmonify.Shared.Models.CommentLike>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return like;
    }
    
    public async Task<Guid?> CreateAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/CommentLike", body);

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
    
    public async Task<bool> DeleteAsync(Guid commentLikeId)
    {
        using var response = await httpClient
            .DeleteAsync($"/CommentLike/{commentLikeId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}