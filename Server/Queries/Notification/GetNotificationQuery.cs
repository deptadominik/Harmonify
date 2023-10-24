using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Notification;

public class GetNotificationQuery : IRequest<Shared.Models.Notification?>
{
    public Guid NotificationId { get; init; }

    public class Handler : IRequestHandler<GetNotificationQuery, Shared.Models.Notification?>
    {
        private readonly NotificationRepository _repository;

        public Handler(NotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Notification?> Handle(GetNotificationQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.NotificationId);
        }
    }
}