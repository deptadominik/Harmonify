using Harmonify.Server.Commands.Notification;
using Harmonify.Server.Queries.Notification;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Notification?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetNotificationQuery { NotificationId = id });
        
        if (entity == null)
            return NoContent();
        
        return Ok(entity);
    }
    
    [HttpGet("my")]
    public async Task<ActionResult<ICollection<Notification>>> GetMyNotifications(string userId)
    {
        var entities = await _mediator
            .Send(new GetMyNotificationsQuery { UserId = userId });
        
        if (entities.Count == 0)
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateNotificationCommand request)
    {
        var notification = await _mediator.Send(new GetNotificationQuery
        {
            NotificationId = request.Id
        });

        if (notification != null)
            return BadRequest("Entity already exists.");

        if (request.ReceivedAt > DateTime.Now)
            return BadRequest($"Cannot create notification with {nameof(request.ReceivedAt)} in future.");
        
        var entity = await _mediator
            .Send(request);
        
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Notification?>> MarkAsSeen(MarkNotificationAsSeenCommand request)
    {
        var notification = await _mediator.Send(new GetNotificationQuery
        {
            NotificationId = request.NotificationId
        });

        if (notification == null)
            return BadRequest("Entity doesn't exist.");

        var entity = await _mediator
            .Send(request);
        
        return entity == null ? BadRequest("Something went wrong.") : Ok(entity);
    }
    
    [HttpPatch("update/all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Notification?>> MarkAllAsSeen(MarkAllNotificationsAsSeenCommand request)
    {
        var updated = await _mediator
            .Send(request);
        
        return updated ? Ok() : BadRequest("Something went wrong.");
    }
}