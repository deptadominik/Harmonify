using Harmonify.Server.Commands.AvatarImage;
using Harmonify.Server.Queries;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AvatarImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public AvatarImageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AvatarImage?>> Get(string userId)
    {
        var entity = await _mediator
            .Send(new GetAvatarImageQuery { UserId = userId });
        
        if (entity == null)
            return NotFound("Avatar not found.");
        
        return Ok(entity);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create(CreateAvatarImageCommand command)
    {
        var entity = await _mediator.Send(command);
        
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var isRemoved = await _mediator
            .Send(new DeleteAvatarImageCommand { AvatarId = id });

        if (!isRemoved)
            return NotFound("Something went wrong, avatar not deleted.");
        
        return Ok();
    }
}