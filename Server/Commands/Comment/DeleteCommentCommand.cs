using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Comment;

public class DeleteCommentCommand : IRequest<bool>
{
    public Guid CommentId { get; init; }

    public class Handler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly CommentRepository _repository;

        public Handler(CommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository.RemoveAsync(request.CommentId);

            return isRemoved;
        }
    }
}