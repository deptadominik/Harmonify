using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Harmonify.Client.Services.Message;

public class MessageService : IMessageService
{
    private readonly HttpClient httpClient;

    public MessageService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.Message?> GetAsync(Guid messageId)
    {
        var response = await httpClient
            .GetAsync($"Message?id={messageId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var message = JsonSerializer.Deserialize<Harmonify.Shared.Models.Message>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return message;
    }

    public async Task<ICollection<Harmonify.Shared.DTO.MessageDTO>> GetMessages(string userId, string otherUserId)
    {
        var response = await httpClient
            .GetAsync($"Message/chat/{userId}/{otherUserId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Harmonify.Shared.DTO.MessageDTO>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<Harmonify.Shared.DTO.MessageDTO>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Harmonify.Shared.DTO.MessageDTO>();
    }
    
    public async Task<ICollection<Harmonify.Shared.DTO.MessageDTO>> GetLastMessages(string userId)
    {
        var response = await httpClient
            .GetAsync($"Message/last?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Harmonify.Shared.DTO.MessageDTO>();
        
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<ICollection<Harmonify.Shared.DTO.MessageDTO>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Harmonify.Shared.DTO.MessageDTO>();
    }
    
    public async Task<Guid?> CreateAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/Message", body);

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

}