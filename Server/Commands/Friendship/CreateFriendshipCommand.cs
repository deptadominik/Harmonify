using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Harmonify.Server.Commands.Friendship;

public class CreateFriendshipCommand: IRequest<Shared.Models.Friendship>
{
    public string MainUserId { get; init; }
    
    public string FriendUserId { get; init; }
    
    public class Handler : IRequestHandler<CreateFriendshipCommand, Shared.Models.Friendship>
    {
        private readonly FriendshipRepository _repository;
        private readonly IMapper _mapper;

        public Handler(FriendshipRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Shared.Models.Friendship> Handle(CreateFriendshipCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Friendship
            {
                MainUserId = request.MainUserId,
                FriendUserId = request.FriendUserId,
                Status = FriendshipStatus.Requested
            };
                
            await _repository.AddFriendshipAsync(entity);
            
            return entity;
        }
    }
}