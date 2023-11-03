using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Post;

public class GetPostQuery : IRequest<Shared.Models.Post?>
{
    public Guid PostId { get; init; }

    public class Handler : IRequestHandler<GetPostQuery, Shared.Models.Post?>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Post?> Handle(GetPostQuery request,
            CancellationToken cancellationToken)
        {
            var post = await _repository.GetAsync(request.PostId);

            return post;
        }
    }
}