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
    /// Avatar of user
    /// </summary>
    public AvatarImage? Avatar { get; set; }
}