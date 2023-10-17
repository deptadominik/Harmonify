using Harmonify.Server.Repositories;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Queries;

public class GetApplicationUserWithFriendsByEmailQuery : IRequest<ApplicationUser?>
{
    public string Email { get; init; }

    public class Handler : IRequestHandler<GetApplicationUserWithFriendsByEmailQuery, ApplicationUser?>
    {
        private readonly ApplicationUserRepository _repository;

        public Handler(ApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationUser?> Handle(GetApplicationUserWithFriendsByEmailQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _repository
                .GetUserWithFriendsByEmailAsync(request.Email);

            return user;
        }
    }
}