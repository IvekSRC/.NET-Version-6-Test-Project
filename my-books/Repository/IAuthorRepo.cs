using my_books.Data.Models.ViewModels;

namespace my_books.Repository
{
    public interface IAuthorRepo
    {
        void AddAuthor(AuthorVM book);
        AuthorWithBooksVM GetAuthorWithBooks(int authorId);
    }
}
