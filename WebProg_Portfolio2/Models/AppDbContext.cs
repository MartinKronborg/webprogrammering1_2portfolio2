using Microsoft.EntityFrameworkCore;

namespace WebProg_Portfolio2.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}
    
    public DbSet<UsersModel> Users { get; set; }
    public DbSet<ImagesModel> Images { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImagesModel>()
            .HasOne(i => i.User)
            .WithMany(u => u.Images)
            .HasForeignKey(i => i.UserId);
    }
    
}