using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetPendingFriendshipsRequestsQuery : IRequest<ICollection<Friendship>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetPendingFriendshipsRequestsQuery, ICollection<Friendship>>
    {
        private readonly FriendshipRepository _repository;

        public Handler(FriendshipRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Friendship>> Handle(GetPendingFriendshipsRequestsQuery request,
            CancellationToken cancellationToken)
        {
            var friendships = await _repository
                .GetMyPendingFriendshipRequestsAsync(request.UserId);

            return friendships;
        }
    }
}