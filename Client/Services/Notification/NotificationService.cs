using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Harmonify.Client.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly HttpClient httpClient;

    public NotificationService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Harmonify.Shared.Models.Notification?> GetAsync(Guid notificationId)
    {
        var response = await httpClient
            .GetAsync($"Notification?id={notificationId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var avatar = JsonSerializer.Deserialize<Harmonify.Shared.Models.Notification>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return avatar;
    }

    public async Task<ICollection<Harmonify.Shared.Models.Notification>> GetMyNotificationsAsync(string userId)
    {
        var response = await httpClient
            .GetAsync($"Notification/my?userId={userId}");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Array.Empty<Harmonify.Shared.Models.Notification>();

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ICollection<Harmonify.Shared.Models.Notification>>(content,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
               ?? Array.Empty<Harmonify.Shared.Models.Notification>();
    }
    
    public async Task<Guid?> CreateAsync(object body)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "/Notification", body);

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

    public async Task<Harmonify.Shared.Models.Notification?> MarkAsSeenAsync(object body)
    {
        using var response = await httpClient.PatchAsJsonAsync(
            "/Notification/update", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => null,
            HttpStatusCode.OK => JsonSerializer.Deserialize<Harmonify.Shared.Models.Notification?>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            _ => null
        };
    }
    
    public async Task<bool> MarkAllAsSeenAsync(object body)
    {
        using var response = await httpClient.PatchAsJsonAsync(
            "/Notification/update/all", body);

        return response.StatusCode switch
        {
            HttpStatusCode.BadRequest => false,
            HttpStatusCode.OK => true,
            _ => false
        };
    }
}