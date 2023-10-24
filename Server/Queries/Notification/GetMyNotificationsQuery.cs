using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Notification;

public class GetMyNotificationsQuery : IRequest<ICollection<Shared.Models.Notification>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetMyNotificationsQuery, ICollection<Shared.Models.Notification>>
    {
        private readonly NotificationRepository _repository;

        public Handler(NotificationRepository repository)
        {
            _repository = repository;
        }

        public Task<ICollection<Shared.Models.Notification>> Handle(GetMyNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetUsersNotifications(request.UserId));
        }
    }
}