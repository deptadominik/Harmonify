using Harmonify.Server.Commands.Post;
using Harmonify.Server.Queries.Post;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<Post?>> Get(Guid id)
    {
        var entity = await _mediator
            .Send(new GetPostQuery { PostId = id });
        
        if (entity == null)
            return NoContent();
        
        return Ok(entity);
    }
    
    [HttpGet("dto")]
    public async Task<ActionResult<PostDTO?>> GetDTO(Guid id)
    {
        var entity = await _mediator
            .Send(new GetPostDtoQuery { PostId = id });
        
        if (entity == null)
            return NoContent();
        
        return Ok(entity);
    }
    
    [HttpGet("my-feed")]
    public async Task<ActionResult<ICollection<PostDTO>>> GetMyFeed(string userId)
    {
        var entities = await _mediator
            .Send(new GetMyFeedQuery { UserId = userId });
        
        if (entities.Count == 0)
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpGet("user-posts")]
    public async Task<ActionResult<ICollection<PostDTO>>> GetUserPosts(string userId)
    {
        var entities = await _mediator
            .Send(new GetUserPostsQuery { UserId = userId });
        
        if (entities.Count == 0)
            return NoContent();
        
        return Ok(entities);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreatePostCommand request)
    {
        var post = await _mediator.Send(new GetPostQuery
        {
            PostId = request.Id
        });

        if (post != null)
            return BadRequest("Entity already exists.");

        if (request.PostedAt > DateTime.Now)
            return BadRequest($"Cannot create post with " +
                              $"{nameof(request.PostedAt)} in future.");
        
        var entity = await _mediator
            .Send(request);
        
        return CreatedAtAction(nameof(Get), 
            new { id = entity.Id }, entity);
    }
    
    [HttpDelete("{postId}")]
    public async Task<ActionResult> Delete(Guid postId)
    {
        var entity = await _mediator
            .Send(new GetPostQuery { PostId = postId });

        if (entity == null)
            return NotFound("Entity doesn't exist.");

        var isRemoved = await _mediator.Send(new DeletePostCommand
        {
            PostId = postId
        });

        if (!isRemoved)
            return NotFound("Something went wrong, Post not deleted.");

        return Ok();
    }
    
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Post?>> Update(UpdatePostContentCommand request)
    {
        var post = await _mediator.Send(new GetPostQuery
        {
            PostId = request.PostId
        });

        if (post == null)
            return BadRequest("Entity doesn't exist.");

        var entity = await _mediator
            .Send(request);
        
        return entity == null ? BadRequest("Something went wrong.") : Ok(entity);
    }
    
    [HttpPatch("update/comments-count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Post?>> Update(UpdateCommentsCountCommand request)
    {
        var post = await _mediator.Send(new GetPostQuery
        {
            PostId = request.PostId
        });

        if (post == null)
            return BadRequest("Entity doesn't exist.");

        var entity = await _mediator
            .Send(request);
        
        return entity == null ? BadRequest("Something went wrong.") : Ok(entity);
    }
}