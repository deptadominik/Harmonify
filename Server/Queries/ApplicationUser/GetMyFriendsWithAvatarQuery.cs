using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetMyFriendsWithAvatarQuery : IRequest<ICollection<ApplicationUserDTO>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetMyFriendsWithAvatarQuery, ICollection<ApplicationUserDTO>>
    {
        private readonly FriendshipRepository _repository;
        private readonly IMapper _mapper;

        public Handler(FriendshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ICollection<ApplicationUserDTO>> Handle(GetMyFriendsWithAvatarQuery request,
            CancellationToken cancellationToken)
        {
            var friends = _repository
                .GetMyFriendsWithAvatar(request.UserId);

            return Task.FromResult(_mapper.Map<ICollection<ApplicationUserDTO>>(friends));
        }
    }
}