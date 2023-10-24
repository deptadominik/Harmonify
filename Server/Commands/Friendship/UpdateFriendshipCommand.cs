using Harmonify.Server.Repositories;
using Harmonify.Shared.Enums;
using Harmonify.Shared.Models;
using MediatR;

namespace Harmonify.Server.Commands.Friendship;

public class UpdateFriendshipCommand: IRequest<Shared.Models.Friendship>
{
    public string MainUserId { get; set; }
    
    public string FriendUserId { get; set; }
    
    public FriendshipStatus Status { get; set; }
    
    public class Handler : IRequestHandler<UpdateFriendshipCommand, Shared.Models.Friendship>
    {
        private readonly FriendshipRepository _repository;

        public Handler(FriendshipRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Friendship> Handle(UpdateFriendshipCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Friendship
            {
                MainUserId = request.MainUserId,
                FriendUserId = request.FriendUserId,
                Status = request.Status
            };
                
            await _repository.UpdateFriendshipAsync(entity);
            
            return entity;
        }
    }
}