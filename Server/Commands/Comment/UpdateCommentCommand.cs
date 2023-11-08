using AutoMapper;
using Harmonify.Server.Repositories;
using Harmonify.Shared.DTO;
using MediatR;

namespace Harmonify.Server.Commands.Comment;

public class UpdateCommentCommand: IRequest<CommentDTO?>
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public DateTime EditedAt { get; set; }
    
    public class Handler : IRequestHandler<UpdateCommentCommand, CommentDTO?>
    {
        private readonly CommentRepository _repository;
        private readonly IMapper _mapper;

        public Handler(CommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommentDTO?> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Comment
            {
                Id = request.Id,
                Content = request.Content,
                EditedAt = request.EditedAt
            };
                
            var wasUpdated = await _repository.UpdateAsync(entity);
            
            return wasUpdated ? _mapper.Map<CommentDTO>(entity) : null;
        }
    }
}