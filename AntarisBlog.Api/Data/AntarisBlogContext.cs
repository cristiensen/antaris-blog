using AntarisBlog.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AntarisBlog.Api.Data {
    public class AntarisBlogContext : DbContext {
        public AntarisBlogContext(DbContextOptions<AntarisBlogContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post!)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Post>()
                .Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
