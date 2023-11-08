using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries.Post;

public class GetUserPostsQuery : IRequest<ICollection<PostDTO>>
{
    public string UserId { get; init; }

    public class Handler : IRequestHandler<GetUserPostsQuery, ICollection<PostDTO>>
    {
        private readonly PostRepository _repository;
        private readonly IMapper _mapper;

        public Handler(PostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<PostDTO>> Handle(GetUserPostsQuery request,
            CancellationToken cancellationToken)
        {
            var posts = await _repository.GetUserPostsAsync(request.UserId);

            return _mapper.Map<ICollection<PostDTO>>(posts);
        }
    }
}