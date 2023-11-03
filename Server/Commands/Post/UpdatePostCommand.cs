using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Post;

public class UpdatePostCommand: IRequest<Shared.Models.Post?>
{
    public Guid PostId { get; set; }
    
    public string Content { get; set; }
    
    public DateTime EditedAt { get; set; }
    
    public class Handler : IRequestHandler<UpdatePostCommand, Shared.Models.Post?>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Post?> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Post
            {
                Id = request.PostId,
                Content = request.Content,
                EditedAt = request.EditedAt
            };
                
            var wasUpdated = await _repository.UpdateAsync(entity);
            
            return wasUpdated ? entity : null;
        }
    }
}