using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<AvatarImage> Avatars { get; set; }  
    
    public DbSet<Friendship> Friendships { get; set; }  
    
    public DbSet<Address> Addresses { get; set; }
    
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<PostLike> PostLikes { get; set; }
    
    public DbSet<PostImage> PostImages { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<CommentLike> CommentLikes { get; set; }
    
    public DbSet<Message> Messages { get; set; }
    
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUser>().ToTable("User");
        builder.Entity<AvatarImage>().ToTable("Avatar");
        builder.Entity<Friendship>().ToTable("Friendship");
        builder.Entity<Address>().ToTable("Address");
        builder.Entity<Notification>().ToTable("Notification");
        builder.Entity<Post>().ToTable("Post");
        builder.Entity<PostLike>().ToTable("PostLike");
        builder.Entity<PostImage>().ToTable("PostImage");
        builder.Entity<Comment>().ToTable("Comment");
        builder.Entity<CommentLike>().ToTable("CommentLike");
        builder.Entity<Message>().ToTable("Message");
        
        builder.Entity<AvatarImage>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Entity<ApplicationUser>()
            .HasOne(a => a.Avatar)
            .WithOne(e => e.User)
            .IsRequired(false)
            .HasForeignKey<AvatarImage>(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<ApplicationUser>()
            .HasOne(a => a.Address)
            .WithOne(e => e.User)
            .IsRequired()
            .HasForeignKey<Address>(n => n.UserId);
        
        builder.Entity<Notification>()
            .HasOne(a => a.User)
            .WithMany(e => e.Notifications)
            .HasForeignKey(n => n.UserId);
        
        builder.Entity<Friendship>()
            .HasKey(p => new { p.MainUserId , p.FriendUserId });

        builder.Entity<Friendship>()
            .HasOne(f => f.MainUser)
            .WithMany(f => f.MainFriends)
            .HasForeignKey(f => f.MainUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Friendship>()
            .HasOne(f => f.FriendUser)
            .WithMany(f => f.Friends)
            .HasForeignKey(f => f.FriendUserId);

        builder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AuthorId);
        
        builder.Entity<PostLike>()
            .HasOne(pl => pl.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(p => p.PostId);
        
        builder.Entity<PostImage>()
            .HasOne(pi => pi.Post)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.PostId);
        
        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<CommentLike>()
            .HasOne(cl => cl.Comment)
            .WithMany(c => c.Likes)
            .HasForeignKey(cl => cl.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Comment>()
            .HasMany(c => c.Replies)
            .WithOne(c => c.ParentComment)
            .HasForeignKey(c => c.ParentCommentId);
        
        builder.Entity<Comment>()
            .HasOne(c => c.Author)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Message>()
            .HasOne(c => c.ToUser)
            .WithMany()
            .HasForeignKey(c => c.ToUserId);
        
        builder.Entity<Message>()
            .HasOne(c => c.FromUser)
            .WithMany()
            .HasForeignKey(c => c.FromUserId);
    }
}