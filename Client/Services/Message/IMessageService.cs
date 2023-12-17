namespace Harmonify.Client.Services.Message;

public interface IMessageService
{
    Task<Harmonify.Shared.Models.Message?> GetAsync(Guid messageId);
    
    Task<Guid?> CreateAsync(object body);
    
    Task<ICollection<Harmonify.Shared.DTO.MessageDTO>> GetMessages(string userId, string otherUserId);
    
    Task<ICollection<Harmonify.Shared.DTO.MessageDTO>> GetLastMessages(string userId);
}