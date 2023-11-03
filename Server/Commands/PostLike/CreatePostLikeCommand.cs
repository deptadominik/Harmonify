using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.PostLike;

public class CreatePostLikeCommand : IRequest<Shared.Models.PostLike>
{
    public Guid Id { get; set; }
    
    public Guid PostId { get; set; }
    
    public string UserId { get; set; }
    
    public class Handler : IRequestHandler<CreatePostLikeCommand, Shared.Models.PostLike>
    {
        private readonly PostLikeRepository _repository;

        public Handler(PostLikeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.PostLike> Handle(CreatePostLikeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.PostLike
            {
                Id = request.Id,
                PostId = request.PostId,
                UserId = request.UserId
            };
        
            await _repository.AddAsync(entity);

            return entity;
        }
    }
}