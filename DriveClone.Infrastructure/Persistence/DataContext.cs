using DriveClone.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveClone.Infrastructure.Persistence;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
    
    public DbSet<FileMetaData> FilesMetaData { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserFolder> UserFolders { get; set; }
    public DbSet<UserFile> UserFiles { get; set; }
}
