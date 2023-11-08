using Harmonify.Server.Repositories;
using MediatR;

namespace Harmonify.Server.Commands.Comment;

public class CreateCommentCommand : IRequest<Shared.Models.Comment>
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public DateTime PostedAt { get; set; }
    
    public Guid PostId { get; set; }
    
    public Guid? ParentCommentId { get; set; }
    
    public string AuthorId { get; set; }
    
    public class Handler : IRequestHandler<CreateCommentCommand, Shared.Models.Comment>
    {
        private readonly CommentRepository _repository;

        public Handler(CommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Comment
            {
                Id = request.Id,
                Content = request.Content,
                PostedAt = request.PostedAt,
                AuthorId = request.AuthorId,
                ParentCommentId = request.ParentCommentId,
                PostId = request.PostId
            };
        
            await _repository.AddAsync(entity);

            return entity;
        }
    }
}