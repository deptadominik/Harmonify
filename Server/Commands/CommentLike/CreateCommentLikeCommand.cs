using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.CommentLike;

public class CreateCommentLikeCommand : IRequest<Shared.Models.CommentLike>
{
    public Guid Id { get; set; }
    
    public Guid CommentId { get; set; }
    
    public string UserId { get; set; }
    
    public class Handler : IRequestHandler<CreateCommentLikeCommand, Shared.Models.CommentLike>
    {
        private readonly CommentLikeRepository _repository;

        public Handler(CommentLikeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.CommentLike> Handle(CreateCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.CommentLike
            {
                Id = request.Id,
                CommentId = request.CommentId,
                UserId = request.UserId
            };
        
            await _repository.AddAsync(entity);

            return entity;
        }
    }
}