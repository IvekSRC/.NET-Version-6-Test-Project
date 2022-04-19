using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Paging;
using PagedList;
using PagedList.Mvc;
using System.Linq;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                isRead = book.isRead,
                DateRead = book.isRead ? book.DateRead.Value : null,
                Rate = book.isRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public List<BookVMBasic> GetAllBooks(PagingProperties prop)
        {
            var searchedBooks = _context.Books.ToList();

            if (prop.StartsWith != null)
            {
                searchedBooks = searchedBooks.Where(x => x.Title.StartsWith(prop.StartsWith)).ToList();
            }

            switch (prop.SortCriterium)
            {
                case "Comedy":
                    searchedBooks = searchedBooks.Where(x => x.Genre == "Comedy").ToList();
                    break;
                case "Biography":
                    searchedBooks = searchedBooks.Where(x => x.Genre == "Biography").ToList();
                    break;
                default:
                    break;
            }

            if (prop.SortOrder == true)
            {
                searchedBooks = searchedBooks.OrderByDescending(x => x.Id).ToList();
            }

            return PaginatedList<BookVMBasic>.Create(searchedBooks.Select(book => new BookVMBasic
            {
                Title = book.Title,
                Description = book.Description,
                isRead = book.isRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl
            }).ToList().AsQueryable(), prop.PageNum, prop.PageSize);
        }

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                isRead = book.isRead,
                DateRead = book.isRead ? book.DateRead.Value : null,
                Rate = book.isRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);

            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.isRead = book.isRead;
                _book.DateRead = book.isRead ? book.DateRead.Value : null;
                _book.Rate = book.isRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _book.DateAdded = DateTime.Now;

                _context.SaveChanges();
            }

            return _book;
        }

        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);

            if(_book != null )
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
     }
}
