using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Commands.AvatarImage;

public class DeleteAvatarImageCommand : IRequest<bool>
{
    public Guid AvatarId { get; init; }

    public class Handler : IRequestHandler<DeleteAvatarImageCommand, bool>
    {
        private readonly AvatarImageRepository _repository;

        public Handler(AvatarImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAvatarImageCommand request, CancellationToken cancellationToken)
        {
            var isRemoved = await _repository.DeleteAvatarImageAsync(request.AvatarId);

            return isRemoved;
        }
    }
}