using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Harmonify.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AvatarImageController : ControllerBase
{
    private readonly ILogger<AvatarImage> _logger;

    public AvatarImageController(ILogger<AvatarImage> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<AvatarImage?>> Get(
        string userId,
        [FromServices] AvatarImageRepository ar)
    {
        var avatar = await ar.GetAvatarImage(userId);

        if (avatar == null)
            return NotFound("Avatar not found.");
        
        else return Ok(avatar);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(
        [FromBody] AvatarImageDTO body,
        [FromServices] AvatarImageRepository ar)
    {
        var id = Guid.NewGuid();
        var entity = new AvatarImage
        {
            Id = id,
            FileName = body.FileName,
            Content = body.Content,
            UserId = body.UserId
        };
        
        var isAdded = await ar.AddAvatarImage(entity);

        if (!isAdded)
            return BadRequest("Something went wrong, avatar not added.");
        
        return CreatedAtAction(nameof(Get), new { id = id }, entity);
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(
        Guid id,
        [FromServices] AvatarImageRepository ar)
    {
        var isRemoved = await ar.DeleteAvatarImage(id);

        if (!isRemoved)
            return NotFound("Something went wrong, avatar not deleted.");
        
        return Ok();
    }
}