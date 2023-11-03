using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Post;

public class GetMyFeedQuery : IRequest<ICollection<Shared.Models.Post>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetMyFeedQuery, ICollection<Shared.Models.Post>>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Shared.Models.Post>> Handle(GetMyFeedQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _repository.GetMyFeedAsync(request.UserId);

            return posts;
        }
    }
}