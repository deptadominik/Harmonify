using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Post;

public class GetUserPostsQuery : IRequest<ICollection<Shared.Models.Post>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetUserPostsQuery, ICollection<Shared.Models.Post>>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Shared.Models.Post>> Handle(GetUserPostsQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _repository.GetUserPostsAsync(request.UserId);

            return posts;
        }
    }
}