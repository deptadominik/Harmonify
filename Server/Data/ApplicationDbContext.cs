using System.ComponentModel.DataAnnotations.Schema;
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
    }
}