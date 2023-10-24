using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Harmonify.Shared.Models;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// First name of user
    /// </summary>
    [MinLength(3, ErrorMessage = "The first name must be at least 3 characters long.")]
    [MaxLength(30, ErrorMessage = "The first name can be maximum 30 characters long.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name of user
    /// </summary>
    [MinLength(3, ErrorMessage = "The last name must be at least 3 characters long.")]
    [MaxLength(30, ErrorMessage = "The last name can be maximum 30 characters long.")]
    public string LastName { get; set; }
    
    /// <summary>
    /// User's birthday
    /// </summary>
    public DateTime? Birthday { get; set; }
    
    /// <summary>
    /// Determines whether birthday should be shown or not
    /// </summary>
    public bool ShowBirthday { get; set; }
    
    /// <summary>
    /// The date when users registered
    /// </summary>
    public DateTime JoinedOn { get; set; }
    
    /// <summary>
    /// Avatar of user
    /// </summary>
    public AvatarImage? Avatar { get; set; }
    
    /// <summary>
    /// Address of user
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// User's notifications
    /// </summary>
    public virtual ICollection<Notification> Notifications { get; set; }
    
    /// <summary>
    /// Friends, which user invited
    /// </summary>
    public virtual ICollection<Friendship> MainFriends { get; set; }
    
    /// <summary>
    /// Users I am friends of (users, which invited me)
    /// </summary>
    public virtual ICollection<Friendship> Friends { get; set; }
}