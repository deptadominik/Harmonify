using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetApplicationUserQuery : IRequest<ApplicationUser?>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetApplicationUserQuery, ApplicationUser?>
    {
        private readonly ApplicationUserRepository _repository;

        public Handler(ApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationUser?> Handle(GetApplicationUserQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.UserId);

            return user;
        }
    }
}