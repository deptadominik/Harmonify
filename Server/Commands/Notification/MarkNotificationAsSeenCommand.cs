using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Notification;

public class MarkNotificationAsSeenCommand: IRequest<Shared.Models.Notification?>
{
    public Guid NotificationId { get; set; }
    
    public bool MarkedAsSeen { get; set; }
    
    public class Handler : IRequestHandler<MarkNotificationAsSeenCommand, Shared.Models.Notification?>
    {
        private readonly NotificationRepository _repository;

        public Handler(NotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Notification?> Handle(MarkNotificationAsSeenCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Notification
            {
                Id = request.NotificationId,
                MarkedAsSeen = request.MarkedAsSeen
            };
                
            var wasUpdated = await _repository.UpdateMarkedAsSeenAsync(entity);
            
            return wasUpdated ? entity : null;
        }
    }
}