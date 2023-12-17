using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries.Message;

public class GetMyChatsQuery : IRequest<ICollection<MessageDTO>>
{
    public string UserId { get; init; }
    

    public class Handler : IRequestHandler<GetMyChatsQuery, ICollection<MessageDTO>>
    {
        private readonly MessageRepository _repository;
        private readonly IMapper _mapper;

        public Handler(MessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<MessageDTO>> Handle(GetMyChatsQuery request,
            CancellationToken cancellationToken)
        {
            var userIds = await _repository
                .GetMyChatUsersAsync(request.UserId);

            var lastMessages = new List<Shared.Models.Message>();

            foreach (var userId in userIds)
            {
                lastMessages.Add((await _repository
                    .GetMessagesAsync(request.UserId, userId))
                    .Last());
            }

            return _mapper.Map<ICollection<MessageDTO>>(lastMessages.Distinct());
        }
    }
}