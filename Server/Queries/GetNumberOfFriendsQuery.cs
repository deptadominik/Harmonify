using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetNumberOfFriendsQuery : IRequest<int>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetNumberOfFriendsQuery, int>
    {
        private readonly FriendshipRepository _repository;

        public Handler(FriendshipRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(GetNumberOfFriendsQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository
                .GetNumberOfFriends(request.UserId));
        }
    }
}