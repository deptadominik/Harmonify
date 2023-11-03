using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.PostLike;

public class GetPostLikeQuery : IRequest<Shared.Models.PostLike?>
{
    public Guid PostLikeId { get; init; }

    public class Handler : IRequestHandler<GetPostLikeQuery, Shared.Models.PostLike?>
    {
        private readonly PostLikeRepository _repository;

        public Handler(PostLikeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.PostLike?> Handle(GetPostLikeQuery request,
            CancellationToken cancellationToken)
        {
            var like = await _repository.GetAsync(request.PostLikeId);

            return like;
        }
    }
}