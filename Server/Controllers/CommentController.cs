using Harmonify.Server.Commands.Comment;
using Harmonify.Server.Queries.Comment;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Comment?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetCommentQuery { CommentId = id });
        
        if (entity == null)
            return NoContent();
        
        return Ok(entity);
    }
    
    [HttpGet("post-comments")]
    public async Task<ActionResult<ICollection<Comment>>> GetPostComments(Guid postId)
    {
        var entities = await _mediator
            .Send(new GetPostCommentsQuery { PostId = postId });
        
        if (entities.Count == 0)
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateCommentCommand request)
    {
        var comment = await _mediator.Send(new GetCommentQuery
        {
            CommentId = request.Id
        });

        if (comment != null)
            return BadRequest("Entity already exists.");

        if (request.PostedAt > DateTime.Now)
            return BadRequest($"Cannot create comment in future - {nameof(request.PostedAt)}");
        
        var entity = await _mediator
            .Send(request);
        
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }
    
    [HttpDelete("{commentId}")]
    public async Task<ActionResult> Delete(Guid commentId)
    {
        var entity = await _mediator
            .Send(new GetCommentQuery { CommentId = commentId });

        if (entity == null)
            return NotFound("Entity doesn't exist.");

        var isRemoved = await _mediator.Send(new DeleteCommentCommand
        {
            CommentId = commentId
        });

        if (!isRemoved)
            return NotFound("Something went wrong, Comment not deleted.");

        return Ok();
    }
    
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CommentDTO?>> Update(UpdateCommentCommand request)
    {
        var comment = await _mediator.Send(new GetCommentQuery
        {
            CommentId = request.Id
        });

        if (comment == null)
            return BadRequest("Entity doesn't exist.");

        var entity = await _mediator
            .Send(request);
        
        return entity == null ? BadRequest("Something went wrong.") : Ok(entity);
    }
}