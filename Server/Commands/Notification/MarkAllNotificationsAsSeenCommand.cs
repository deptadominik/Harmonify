using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Notification;

public class MarkAllNotificationsAsSeenCommand: IRequest<bool>
{
    public string UserId { get; set; }
    
    public class Handler : IRequestHandler<MarkAllNotificationsAsSeenCommand, bool>
    {
        private readonly NotificationRepository _repository;

        public Handler(NotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(MarkAllNotificationsAsSeenCommand request, CancellationToken cancellationToken)
        {
            var updated = await _repository.MarkAllAsSeen(request.UserId);
            
            return updated;
        }
    }
}