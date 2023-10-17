using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Friendship;

public class DeleteFriendshipCommand : IRequest<bool>
{
    public string UserId { get; init; }
    
    public string FriendId { get; init; }

    public class Handler : IRequestHandler<DeleteFriendshipCommand, bool>
    {
        private readonly FriendshipRepository _repository;

        public Handler(FriendshipRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteFriendshipCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository
                .DeleteFriendshipAsync(request.UserId, request.FriendId);

            return isRemoved;
        }
    }
}