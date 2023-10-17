using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Commands.AvatarImage;

public class CreateAvatarImageCommand: IRequest<AvatarImageDTO>
{
    public string FileName { get; init; }
    
    public byte[] Content { get; init; }
    
    public string UserId { get; init; }
    
    public class Handler : IRequestHandler<CreateAvatarImageCommand, AvatarImageDTO>
    {
        private readonly AvatarImageRepository _repository;
        private readonly IMapper _mapper;

        public Handler(AvatarImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AvatarImageDTO> Handle(CreateAvatarImageCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var entity = new Shared.Models.AvatarImage
            {
                Id = id,
                FileName = request.FileName,
                Content = request.Content,
                UserId = request.UserId
            };
        
            await _repository.AddAvatarImageAsync(entity);
            
            return _mapper.Map<AvatarImageDTO>(entity);
        }
    }
}