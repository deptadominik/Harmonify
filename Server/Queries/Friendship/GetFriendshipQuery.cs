using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetFriendshipQuery : IRequest<Friendship?>
{
    public string MainUserId { get; init; }
    
    public string FriendUserId { get; init; }

    public class Handler : IRequestHandler<GetFriendshipQuery, Friendship?>
    {
        private readonly FriendshipRepository _repository;

        public Handler(FriendshipRepository repository)
        {
            _repository = repository;
        }

        public async Task<Friendship?> Handle(GetFriendshipQuery request,
            CancellationToken cancellationToken)
        {
            var friendship = await _repository
                .GetFriendshipAsync(request.MainUserId, request.FriendUserId);

            return friendship;
        }
    }
}