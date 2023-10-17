using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetApplicationUserByEmailQuery : IRequest<ApplicationUserDTO?>
{
    public string Email { get; init; }

    public class Handler : IRequestHandler<GetApplicationUserByEmailQuery, ApplicationUserDTO?>
    {
        private readonly ApplicationUserRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ApplicationUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDTO?> Handle(GetApplicationUserByEmailQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByEmailAsync(request.Email);

            return _mapper.Map<ApplicationUserDTO>(user);
        }
    }
}