using my_books.Data.Models.ViewModels;
using my_books.Repository;

namespace my_books.Data.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepo _authorRepo;

        public AuthorService(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public void AddAuthor(AuthorVM book)
        {
            _authorRepo.AddAuthor(book);
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            return _authorRepo.GetAuthorWithBooks(authorId);
        }
    }
}
