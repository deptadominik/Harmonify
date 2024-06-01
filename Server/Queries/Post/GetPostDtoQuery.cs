using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries.Post;

public class GetPostDtoQuery : IRequest<PostDTO?>
{
    public Guid PostId { get; init; }

    public class Handler : IRequestHandler<GetPostDtoQuery, PostDTO?>
    {
        private readonly PostRepository _repository;
        private readonly IMapper _mapper;

        public Handler(PostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PostDTO?> Handle(GetPostDtoQuery request,
            CancellationToken cancellationToken)
        {
            var post = await _repository.GetAsync(request.PostId);

            return _mapper.Map<PostDTO>(post);
        }
    }
}