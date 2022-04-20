using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Paging;

namespace my_books.Repository
{
    public interface IBookRepo
    {
        void AddBookWithAuthors(BookVM book);
        List<BookVMBasic> GetAllBooks(PagingProperties prop);
        BookWithAuthorsVM GetBookById(int bookId);
        Book UpdateBookById(int bookId, BookVM book);
        void DeleteBookById(int bookId);
    }
}
