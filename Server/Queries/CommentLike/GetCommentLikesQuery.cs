using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries.CommentLike;

public class GetCommentLikesQuery : IRequest<ICollection<CommentLikeDTO>>
{
    public Guid CommentId { get; init; }

    public class Handler : IRequestHandler<GetCommentLikesQuery, ICollection<CommentLikeDTO>>
    {
        private readonly CommentLikeRepository _repository;
        private readonly IMapper _mapper;

        public Handler(CommentLikeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<CommentLikeDTO>> Handle(GetCommentLikesQuery request,
            CancellationToken cancellationToken)
        {
            var likes = await _repository.GetLikesAsync(request.CommentId);

            return _mapper.Map<ICollection<CommentLikeDTO>>(likes);
        }
    }
}