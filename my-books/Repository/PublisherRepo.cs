using my_books.Data.Models;
using my_books.Data.Models.ViewModels;

namespace my_books.Repository
{
    public class PublisherRepo : IPublisherRepo
    {
        private readonly AppDbContext _context;

        public PublisherRepo(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher? GetPublisherById(int id)
        {
            if(id > 0)
            {
                return _context.Publishers.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return null;
            }
        }

        public PublisherWithBooksAndAuthorsVM? GetPublisherData(int publisherId)
        {            
            var foundPublisher = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            if (foundPublisher != null)
                return foundPublisher;
            else
                return null;
        }

        public Boolean DeletePublisherById(int publisherId)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == publisherId);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
