using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetApplicationUsersByPartialNameQuery : IRequest<ICollection<ApplicationUserDTO>>
{
    public string Phrase { get; init; }

    public class Handler : IRequestHandler<GetApplicationUsersByPartialNameQuery, ICollection<ApplicationUserDTO>>
    {
        private readonly ApplicationUserRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ApplicationUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<ApplicationUserDTO>> Handle(GetApplicationUsersByPartialNameQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _repository
                .GetUsersByPartialNameAsync(request.Phrase);

            return _mapper.Map<ICollection<ApplicationUserDTO>>(users);
        }
    }
}