using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.PostImage;

public class GetPostImageQuery : IRequest<Shared.Models.PostImage?>
{
    public Guid Id { get; init; }

    public class Handler : IRequestHandler<GetPostImageQuery, Shared.Models.PostImage?>
    {
        private readonly PostImageRepository _repository;

        public Handler(PostImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.PostImage?> Handle(GetPostImageQuery request,
            CancellationToken cancellationToken)
        {
            var image = await _repository.GetAsync(request.Id);

            return image;
        }
    }
}