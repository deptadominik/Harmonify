using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Queries.Comment;

public class GetPostCommentsQuery : IRequest<ICollection<CommentDTO>>
{
    public Guid PostId { get; init; }

    public class Handler : IRequestHandler<GetPostCommentsQuery, ICollection<CommentDTO>>
    {
        private readonly CommentRepository _repository;
        private readonly IMapper _mapper;

        public Handler(CommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<CommentDTO>> Handle(GetPostCommentsQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await _repository
                .GetPostCommentsAsync(request.PostId);

            return _mapper.Map<ICollection<CommentDTO>>(comments);
        }
    }
}