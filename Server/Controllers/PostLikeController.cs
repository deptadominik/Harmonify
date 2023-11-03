using Harmonify.Server.Commands.PostLike;
using Harmonify.Server.Queries.PostLike;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PostLikeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostLikeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PostLike?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetPostLikeQuery { PostLikeId = id });

        if (entity == null)
            return NoContent();

        return Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreatePostLikeCommand request)
    {
        var like = await _mediator.Send(new GetPostLikeQuery
        {
            PostLikeId = request.Id
        });

        if (like != null)
            return BadRequest("Entity already exists.");

        var entity = await _mediator
            .Send(request);

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpDelete("{postLikeId}")]
    public async Task<ActionResult> Delete(Guid postLikeId)
    {
        var entity = await _mediator
            .Send(new GetPostLikeQuery { PostLikeId = postLikeId });

        if (entity == null)
            return NotFound("Entity doesn't exist.");

        var isRemoved = await _mediator.Send(new DeletePostLikeCommand
        {
            PostLikeId = postLikeId
        });

        if (!isRemoved)
            return NotFound("Something went wrong, PostLike not deleted.");

        return Ok();
    }
}