using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;

namespace Harmonify.Client.Services.CommentService;

public class CommentService : ICommentService
{
    private readonly HttpClient httpClient;

    public CommentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.Comment?> GetAsync(Guid commentId)
    {
        var response = await httpClient
            .GetAsync($"Comment?id={commentId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var comment = JsonSerializer.Deserialize<Harmonify.Shared.Models.Comment>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return comment;
    }
    
    public async Task<ICollection<CommentDTO>> GetPostCommentsAsync(Guid postId)
    {
        var response = await httpClient
            .GetAsync($"Comment/post-comments?postId={postId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<CommentDTO>();

        var content = await response.Content.ReadAsStringAsync();

        var comments = JsonSerializer.Deserialize<ICollection<CommentDTO>>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return comments ?? Array.Empty<CommentDTO>();
    }
    
    public async Task<Guid?> CreateAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/Comment", body);

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
    
    public async Task<CommentDTO?> UpdateAsync(object body)
    {
        using var response = await httpClient.PatchAsJsonAsync(
            "/Comment/update", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.OK => JsonSerializer.Deserialize<CommentDTO?>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            _ => null
        };
    }
    
    public async Task<bool> DeleteAsync(Guid commentId)
    {
        using var response = await httpClient
            .DeleteAsync($"/Comment/{commentId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}