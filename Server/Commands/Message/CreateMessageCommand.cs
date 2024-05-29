using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Message;

public class CreateMessageCommand : IRequest<Shared.Models.Message>
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public string FromUserId { get; set; }
    
    public string ToUserId { get; set; }
    
    public DateTime SentOn { get; set; }
    
    public class Handler : IRequestHandler<CreateMessageCommand, Shared.Models.Message>
    {
        private readonly MessageRepository _repository;

        public Handler(MessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Message> Handle(
            CreateMessageCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Message
            {
                Id = request.Id,
                Content = request.Content,
                FromUserId = request.FromUserId,
                ToUserId = request.ToUserId,
                SentOn = request.SentOn
            };
        
            await _repository.AddAsync(entity);

            return entity;
        }
    }
}