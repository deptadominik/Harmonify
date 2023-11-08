using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Harmonify.Shared.DTO;

namespace Harmonify.Client.Services.Post;

public class PostService : IPostService
{
    private readonly HttpClient httpClient;

    public PostService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.Post?> GetAsync(Guid postId)
    {
        var response = await httpClient
            .GetAsync($"Post?id={postId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var post = JsonSerializer.Deserialize<Harmonify.Shared.Models.Post>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return post;
    }

    public async Task<ICollection<PostDTO>> GetMyFeedAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Post/my-feed?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<PostDTO>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<PostDTO>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<PostDTO>();
    }
    
    public async Task<ICollection<PostDTO>> GetUserPostsAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Post/user-posts?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<PostDTO>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<PostDTO>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<PostDTO>();
    }
    
    public async Task<Guid?> CreateAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/Post", body);

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
    
    public async Task<bool> DeleteAsync(Guid postId)
    {
        using var response = await httpClient
            .DeleteAsync($"/Post/{postId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }

    public async Task<Harmonify.Shared.Models.Post?> UpdateAsync(object body)
    {
        using var response = await httpClient.PatchAsJsonAsync(
            "/Post/update", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.OK => JsonSerializer.Deserialize<Harmonify.Shared.Models.Post?>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            _ => null
        };
    }
}