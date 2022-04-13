using my_books.Data.Models;
using my_books.Data.Models.ViewModels;

namespace my_books.Data.Services
{
    public class AuthorService
    {
        private AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM book)
        {
            var _author = new Author()
            {
                FullName = book.FullName,
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }
    }
}
