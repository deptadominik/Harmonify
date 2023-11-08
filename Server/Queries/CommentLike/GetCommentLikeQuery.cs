using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.CommentLike;

public class GetCommentLikeQuery : IRequest<Shared.Models.CommentLike?>
{
    public Guid CommentLikeId { get; init; }

    public class Handler : IRequestHandler<GetCommentLikeQuery, Shared.Models.CommentLike?>
    {
        private readonly CommentLikeRepository _repository;

        public Handler(CommentLikeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.CommentLike?> Handle(GetCommentLikeQuery request,
            CancellationToken cancellationToken)
        {
            var like = await _repository.GetAsync(request.CommentLikeId);

            return like;
        }
    }
}