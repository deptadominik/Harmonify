using AutoMapper;
using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.PostImage;

public class CreatePostImagesCommand : IRequest<bool>
{
    public ICollection<Shared.DTO.PostImageDTO> PostImages { get; set; }
    
    public class Handler : IRequestHandler<CreatePostImagesCommand, bool>
    {
        private readonly PostImageRepository _repository;
        private readonly IMapper _mapper;

        public Handler(PostImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePostImagesCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository
                .AddAsync(_mapper.Map<ICollection<Shared.Models.PostImage>>(request.PostImages));

            return result;
        }
    }
}