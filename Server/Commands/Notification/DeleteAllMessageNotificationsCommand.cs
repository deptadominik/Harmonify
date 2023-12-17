using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Notification;

public class DeleteAllMessageNotificationsCommand: IRequest<bool>
{
    public string UserId { get; set; }
    
    public class Handler : IRequestHandler<DeleteAllMessageNotificationsCommand, bool>
    {
        private readonly NotificationRepository _repository;

        public Handler(NotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAllMessageNotificationsCommand request, CancellationToken cancellationToken)
        {
            var removed = await _repository.DeleteAllMessageNotifications(request.UserId);
            
            return removed;
        }
    }
}