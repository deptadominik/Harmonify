using Harmonify.Server.Queries.Post;
using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Queries.Message;

public class GetMessageQuery : IRequest<Shared.Models.Message?>
{
    public Guid MessageId { get; init; }

    public class Handler : IRequestHandler<GetMessageQuery, Shared.Models.Message?>
    {
        private readonly MessageRepository _repository;

        public Handler(MessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Message?> Handle(GetMessageQuery request,
            CancellationToken cancellationToken)
        {
            var post = await _repository.GetAsync(request.MessageId);

            return post;
        }
    }
}