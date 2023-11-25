using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.PostImage;

public class DeletePostImageCommand : IRequest<bool>
{
    public Guid PostImageId { get; init; }

    public class Handler : IRequestHandler<DeletePostImageCommand, bool>
    {
        private readonly PostImageRepository _repository;

        public Handler(PostImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePostImageCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository.RemoveAsync(request.PostImageId);

            return isRemoved;
        }
    }
}