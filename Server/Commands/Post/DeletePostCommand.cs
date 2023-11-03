using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Post;

public class DeletePostCommand : IRequest<bool>
{
    public Guid PostId { get; init; }

    public class Handler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository.RemoveAsync(request.PostId);

            return isRemoved;
        }
    }
}