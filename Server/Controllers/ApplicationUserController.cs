using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationUserController : ControllerBase
{
    private readonly ILogger<ApplicationUser> _logger;

    public ApplicationUserController(ILogger<ApplicationUser> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<ApplicationUser?>> GetAccountByEmail(
        string email,
        [FromServices] ApplicationUserRepository ur)
    {
        var user = await ur.GetUserByEmailAsync(email);

        if (user == null)
            return NotFound("User not found.");
        else return Ok(user);
    }
}