using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Harmonify.Shared.Models;

namespace Harmonify.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<AvatarImage> Avatar { get; set; }  
    
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUser>().ToTable("User");
        
        builder.Entity<AvatarImage>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<ApplicationUser>()
            .HasOne(a => a.Avatar)
            .WithOne(e => e.User)
            .IsRequired(false)
            .HasForeignKey<AvatarImage>(n => n.UserId);
    }
}