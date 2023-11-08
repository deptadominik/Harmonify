using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.CommentLike;

public class DeleteCommentLikeCommand : IRequest<bool>
{
    public Guid CommentLikeId { get; init; }

    public class Handler : IRequestHandler<DeleteCommentLikeCommand, bool>
    {
        private readonly CommentLikeRepository _repository;

        public Handler(CommentLikeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCommentLikeCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository.RemoveAsync(request.CommentLikeId);

            return isRemoved;
        }
    }
}