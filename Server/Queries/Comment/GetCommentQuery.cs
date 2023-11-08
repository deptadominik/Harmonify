using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Comment;

public class GetCommentQuery : IRequest<Shared.Models.Comment?>
{
    public Guid CommentId { get; init; }

    public class Handler : IRequestHandler<GetCommentQuery, Shared.Models.Comment?>
    {
        private readonly CommentRepository _repository;

        public Handler(CommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Comment?> Handle(GetCommentQuery request,
            CancellationToken cancellationToken)
        {
            var comment = await _repository.GetAsync(request.CommentId);

            return comment;
        }
    }
}