using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Post;

public class UpdateCommentsCountCommand: IRequest<Shared.Models.Post?>
{
    public Guid PostId { get; set; }
    
    public int CommentsCount { get; set; }
    
    public class Handler : IRequestHandler<UpdateCommentsCountCommand, Shared.Models.Post?>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Post?> Handle(UpdateCommentsCountCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Post
            {
                Id = request.PostId,
                CommentsCount = request.CommentsCount
            };
                
            var wasUpdated = await _repository.UpdateCommentsCountAsync(entity);
            
            return wasUpdated ? entity : null;
        }
    }
}