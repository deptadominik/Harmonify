using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Post;

public class UpdatePostContentCommand: IRequest<Shared.Models.Post?>
{
    public Guid PostId { get; set; }
    
    public string Content { get; set; }
    
    public DateTime EditedAt { get; set; }
    
    public class Handler : IRequestHandler<UpdatePostContentCommand, Shared.Models.Post?>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Post?> Handle(UpdatePostContentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Post
            {
                Id = request.PostId,
                Content = request.Content,
                EditedAt = request.EditedAt
            };
                
            var wasUpdated = await _repository.UpdateContentAsync(entity);
            
            return wasUpdated ? entity : null;
        }
    }
}