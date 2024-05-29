using Harmonify.Server.Queries;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationUserController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApplicationUserDTO?>> Get(string userId)
    {
        var entity = await _mediator
            .Send(new GetApplicationUserQuery { UserId = userId });
        
        if (entity == null)
            return NoContent();
        
        return Ok(entity);
    }
    
    [HttpGet("by-email")]
    public async Task<ActionResult<ApplicationUserDTO?>> GetByEmail(string email)
    {
        var entity = await _mediator
            .Send(new GetApplicationUserByEmailQuery { Email = email });
        
        if (entity == null)
            return NotFound("User not found.");
        
        return Ok(entity);
    }
    
    [HttpGet("by-phrase")]
    public async Task<ActionResult<ApplicationUserDTO?>> GetByPhrase(string phrase)
    {
        var entities = await _mediator
            .Send(new GetApplicationUsersByPartialNameQuery { Phrase = phrase });
        
        return Ok(entities);
    }
}