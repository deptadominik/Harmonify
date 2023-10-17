using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetFriendshipsQuery : IRequest<ICollection<Friendship>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetFriendshipsQuery, ICollection<Friendship>>
    {
        private readonly FriendshipRepository _repository;

        public Handler(FriendshipRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Friendship>> Handle(GetFriendshipsQuery request,
            CancellationToken cancellationToken)
        {
            var friendships = await _repository
                .GetFriendshipsAsync(request.UserId);

            return friendships;
        }
    }
}