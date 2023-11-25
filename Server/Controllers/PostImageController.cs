using Harmonify.Server.Commands.PostImage;
using Harmonify.Server.Queries.PostImage;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PostImageController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostImageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<PostImage?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetPostImageQuery { Id = id });

        if (entity == null)
            return NoContent();

        return Ok(entity);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreatePostImagesCommand request)
    {
        foreach (var image in request.PostImages)
        {
            var entity = await _mediator.Send(new GetPostImageQuery
            {
                Id = image.Id
            });

            if (entity != null)
                return BadRequest(
                    $"Entity of type {typeof(PostImage)} (ID: {image.Id}) already exists.");
        }

        var result = await _mediator
            .Send(request);

        return result ? Created(nameof(Get), request.PostImages) : NotFound();
    }
    
    [HttpDelete("{postImageId}")]
    public async Task<ActionResult> Delete(Guid postImageId)
    {
        var entity = await _mediator
            .Send(new GetPostImageQuery { Id = postImageId });

        if (entity == null)
            return NotFound("Entity doesn't exist.");

        var isRemoved = await _mediator.Send(new DeletePostImageCommand
        {
            PostImageId = postImageId
        });

        if (!isRemoved)
            return NotFound("Something went wrong, PostImage not deleted.");

        return Ok();
    }
}