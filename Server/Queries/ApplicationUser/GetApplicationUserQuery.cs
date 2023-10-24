using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetApplicationUserQuery : IRequest<ApplicationUserDTO?>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetApplicationUserQuery, ApplicationUserDTO?>
    {
        private readonly ApplicationUserRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ApplicationUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDTO?> Handle(GetApplicationUserQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.UserId);

            return _mapper.Map<ApplicationUserDTO>(user);
        }
    }
}