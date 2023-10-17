using Harmonify.Server.Commands.Friendship;
using Harmonify.Server.Queries;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FriendshipController : ControllerBase
{
    private readonly IMediator _mediator;

    public FriendshipController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Friendship>>> Get(string userId)
    {
        var entities = await _mediator
            .Send(new GetFriendshipsQuery { UserId = userId });
        
        if (entities.IsNullOrEmpty())
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpGet("my-friends/with-avatar")]
    public async Task<ActionResult<ICollection<ApplicationUserDTO>>> GetMyFriendsWithAvatar(string userId)
    {
        var entities = await _mediator
            .Send(new GetMyFriendsWithAvatarQuery { UserId = userId });
        
        if (entities.IsNullOrEmpty())
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpGet("number-of-friends")]
    public async Task<ActionResult<int>> GetNumberOfFriends(string userId)
    {
        var numberOfFriends = await _mediator
            .Send(new GetNumberOfFriendsQuery { UserId = userId });
        
        return Ok(numberOfFriends);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateFriendshipCommand request)
    {
        var friendship = await _mediator.Send(new GetFriendshipQuery
        {
            MainUserId = request.MainUserId,
            FriendUserId = request.FriendUserId
        });

        if (friendship != null)
            return BadRequest("Entity already exists.");

        if (request.MainUserId == request.FriendUserId)
            return BadRequest("Cannot create friendship between same users.");
        
        var entity = await _mediator
            .Send(request);
        
        return CreatedAtAction(nameof(Get), new { mainUserId = entity.MainUserId }, entity);
    }
    
    [HttpDelete("{mainUserId}/{friendUserId}")]
    public async Task<ActionResult> Delete(string mainUserId, string friendUserId)
    {
        var isRemoved = await _mediator
            .Send(new DeleteFriendshipCommand
            {
                UserId = mainUserId,
                FriendId = friendUserId
            });

        if (!isRemoved)
            return NotFound("Something went wrong, friendship not deleted.");
        
        return Ok();
    }
}