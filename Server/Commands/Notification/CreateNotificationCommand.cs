using Harmonify.Server.Repositories;
using Harmonify.Shared.Enums;
using MediatR;

namespace Harmonify.Server.Commands.Notification;

public class CreateNotificationCommand : IRequest<Shared.Models.Notification>
{
    public Guid Id { get; set; }
    
    public NotificationType Type { get; set; }
    
    public string Description { get; set; }
    
    public string ReferenceUrl { get; set; }
    
    public DateTime ReceivedAt { get; init; }
    
    public bool MarkedAsSeen { get; set; }
    
    public string UserId { get; init; }
    
    public class Handler : IRequestHandler<CreateNotificationCommand, Shared.Models.Notification>
    {
        private readonly NotificationRepository _repository;

        public Handler(NotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Notification> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Notification
            {
                Id = request.Id,
                Description = request.Description,
                ReceivedAt = request.ReceivedAt,
                MarkedAsSeen = request.MarkedAsSeen,
                ReferenceUrl = request.ReferenceUrl,
                Type = request.Type,
                UserId = request.UserId
            };
        
            await _repository.AddAsync(entity);

            return entity;
        }
    }
}