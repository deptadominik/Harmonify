using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetAvatarImageQuery : IRequest<AvatarImageDTO?>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetAvatarImageQuery, AvatarImageDTO?>
    {
        private readonly AvatarImageRepository _repository;
        private readonly IMapper _mapper;

        public Handler(AvatarImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AvatarImageDTO?> Handle(GetAvatarImageQuery request,
            CancellationToken cancellationToken)
        {
            var avatar = await _repository.GetAvatarImageAsync(request.UserId);

            return avatar == null ? null : _mapper.Map<AvatarImageDTO>(avatar);
        }
    }
}