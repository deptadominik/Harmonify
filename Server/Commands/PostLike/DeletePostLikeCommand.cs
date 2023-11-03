using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.PostLike;

public class DeletePostLikeCommand : IRequest<bool>
{
    public Guid PostLikeId { get; init; }

    public class Handler : IRequestHandler<DeletePostLikeCommand, bool>
    {
        private readonly PostLikeRepository _repository;

        public Handler(PostLikeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePostLikeCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository.RemoveAsync(request.PostLikeId);

            return isRemoved;
        }
    }
}