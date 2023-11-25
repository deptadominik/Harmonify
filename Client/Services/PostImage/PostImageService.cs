using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Harmonify.Client.Services.PostImage;

public class PostImageService : IPostImageService
{
    private readonly HttpClient httpClient;

    public PostImageService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.PostImage?> GetAsync(Guid imageId)
    {
        var response = await httpClient
            .GetAsync($"PostImage?id={imageId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var image = JsonSerializer.Deserialize<Harmonify.Shared.Models.PostImage>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return image;
    }
    
    public async Task<ICollection<Harmonify.Shared.Models.PostImage>> CreateAsync(object images)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/PostImage", images);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => Array.Empty<Harmonify.Shared.Models.PostImage>(),
            HttpStatusCode.Created => (await response.Content
                    .ReadFromJsonAsync<JsonElement>())
                .Deserialize<ICollection<Harmonify.Shared.Models.PostImage>>(),
            _ => Array.Empty<Harmonify.Shared.Models.PostImage>()
        };
    }
    
    public async Task<bool> DeleteAsync(Guid postImageId)
    {
        using var response = await httpClient
            .DeleteAsync($"/PostImage/{postImageId}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}