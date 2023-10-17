namespace Harmonify.Shared.DTO;

public class ApplicationUserDTO
{
    public string Id { get; set; }
    
    public string FullName { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    
    public string Description { get; set; }
    
    public string Email { get; set; }
    
    public string City { get; set; }
    
    public string? Birthday { get; set; }
    
    public string JoinedOn { get; set; }
    
    public string ProfileUrl { get; set; }
    
    public byte[]? AvatarContent { get; set; }
    
    public string? AvatarFileName { get; set; }
    
    public string? AvatarSource { get; set; }
}