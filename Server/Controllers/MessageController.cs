using Duende.IdentityServer.Extensions;
using Harmonify.Server.Commands.Message;
using Harmonify.Server.Hubs;
using Harmonify.Server.Queries.Message;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Hub;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<ChatHub, IChatHubClient> _hubContext;

    public MessageController(IMediator mediator, IHubContext<ChatHub, IChatHubClient> _hubContext)
    {
        _mediator = mediator;
        this._hubContext = _hubContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<Message?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetMessageQuery { MessageId = id });
        
        if (entity == null)
            return NoContent();
        
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageCommand request)
    {
        if (string.IsNullOrWhiteSpace(request.FromUserId) || string.IsNullOrWhiteSpace(request.ToUserId))
            return BadRequest();

        var entity = await _mediator
            .Send(request);

        var msg = new MessageDTO
        {
            Id = entity.Id,
            SentOn = entity.SentOn,
            FromUserId = entity.FromUserId,
            ToUserId = entity.ToUserId,
            Content = entity.Content
        };
        
        await _hubContext.Clients.All.MessageReceived(entity.ToUserId, msg);
        
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpGet("chat/{userId}/{otherUserId}")]
    public async Task<ActionResult<ICollection<MessageDTO>>> GetMessages(string userId, string otherUserId)
    {
        var entities = await _mediator
            .Send(new GetMyChatQuery { UserId = userId, OtherUserId = otherUserId });
        
        if (entities.Count == 0)
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpGet("last")]
    public async Task<ActionResult<ICollection<MessageDTO>>> GetMyChatsLastMessages(string userId)
    {
        var entities = await _mediator
            .Send(new GetMyChatsQuery { UserId = userId });
        
        if (entities.IsNullOrEmpty())
            return NoContent();
        
        return Ok(entities);
    }
}