using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Paging;
using my_books.Repository;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private readonly IBookRepo _bookRepo;

        public BooksService(BookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public void AddBookWithAuthors(BookVM book)
        {
            _bookRepo.AddBookWithAuthors(book);
        }

        public List<BookVMBasic> GetAllBooks(PagingProperties prop)
        {
            return _bookRepo.GetAllBooks(prop);
        }

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            return _bookRepo.GetBookById(bookId);
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            return _bookRepo.UpdateBookById(bookId, book);
        }

        public void DeleteBookById(int bookId)
        {
            _bookRepo.DeleteBookById(bookId);
        }
     }
}
