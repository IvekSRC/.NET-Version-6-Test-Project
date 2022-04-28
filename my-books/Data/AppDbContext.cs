using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace my_books.Data.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(b => b.Author)
                .WithMany(ba => ba.Book_Authors)
                .HasForeignKey(bi => bi.AuthorId);

            //modelBuilder.Entity<Book_Author>()
            //    .Property(x => x.Contributors)
            //    .HasConversion(x => string.Join('|', x),
            //                   x => x.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder
                .Entity<Publisher>()
                .ToTable("Publishers", b => b.IsTemporal());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book_Author> Books_Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
