namespace Harmonify.Client.Services.PostImage;

public interface IPostImageService
{
    Task<Harmonify.Shared.Models.PostImage?> GetAsync(Guid likeId);
    
    Task<ICollection<Harmonify.Shared.Models.PostImage>> CreateAsync(object images);
    
    Task<bool> DeleteAsync(Guid postImageId);
}