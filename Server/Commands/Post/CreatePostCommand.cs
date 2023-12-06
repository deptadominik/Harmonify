using Harmonify.Server.Repositories;
using Harmonify.Shared.Enums;
using MediatR;

namespace Harmonify.Server.Commands.Post;

public class CreatePostCommand : IRequest<Shared.Models.Post>
{
    public Guid Id { get; set; }
    
    public string? Content { get; set; }
    
    public PostType Type { get; set; }
    
    public DateTime PostedAt { get; set; }
    
    public string AuthorId { get; set; }
    
    public class Handler : IRequestHandler<CreatePostCommand, Shared.Models.Post>
    {
        private readonly PostRepository _repository;

        public Handler(PostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Shared.Models.Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = new Shared.Models.Post
            {
                Id = request.Id,
                Content = request.Content,
                Type = request.Type,
                PostedAt = request.PostedAt,
                AuthorId = request.AuthorId,
                CommentsCount = 0
            };
        
            await _repository.AddAsync(entity);

            return entity;
        }
    }
}