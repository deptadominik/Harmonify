using Harmonify.Server.Commands.CommentLike;
using Harmonify.Server.Queries.CommentLike;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentLikeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentLikeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<CommentLike?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetCommentLikeQuery { CommentLikeId = id });

        if (entity == null)
            return NoContent();

        return Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateCommentLikeCommand request)
    {
        var like = await _mediator.Send(new GetCommentLikeQuery
        {
            CommentLikeId = request.Id
        });

        if (like != null)
            return BadRequest("Entity already exists.");

        var entity = await _mediator
            .Send(request);

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpDelete("{commentLikeId}")]
    public async Task<ActionResult> Delete(Guid commentLikeId)
    {
        var entity = await _mediator
            .Send(new GetCommentLikeQuery { CommentLikeId = commentLikeId });

        if (entity == null)
            return NotFound("Entity doesn't exist.");

        var isRemoved = await _mediator.Send(new DeleteCommentLikeCommand
        {
            CommentLikeId = commentLikeId
        });

        if (!isRemoved)
            return NotFound("Something went wrong, CommentLike not deleted.");

        return Ok();
    }
}