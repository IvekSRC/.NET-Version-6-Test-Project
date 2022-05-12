using AutoMapper;
using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;
using my_books.Data.Models.ViewModels;

namespace my_books.Repository
{
    public class PublisherRepo : IPublisherRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PublisherRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                foreach (var book in _context.Books.Where(b => b.PublisherId == publisherId))
                {
                    book.PublisherId = null;
                    book.Publisher = null;
                }

                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ReturnDeletedPublisher(int publisherId)
        {

            var _publisher = _context
                .Publishers
                .TemporalAll()
                .Where(e => e.Id == publisherId)
                .FirstOrDefault();

            if (_publisher is not null)
            {
                _context.Database.OpenConnection();
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers ON;");
                    _context.Publishers.Add(_publisher);
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers OFF;");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Publisher> TestFullSearch(string forSearch)
        {
            var query = _context.Publishers.Where(x => EF.Functions.FreeText(x.Name, forSearch)).ToList();
            //var searchString = "%" + forSearch + "%";
            //var query = from e in _context.Publishers
            //            where EF.Functions.Like(e.Name, searchString) 
            //            select e;

            var result = query.ToList();

            if(query is not null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<PublisherVM>? GetAllPublisher()
        {
            var returnedPublishers = _context.Publishers.ToList();
            var returnedPublishersVM = new List<PublisherVM>();

            if (returnedPublishers is not null)
            {
                foreach (var item in returnedPublishers)
                {
                    returnedPublishersVM.Add(_mapper.Map<PublisherVM>(item));
                }

                return returnedPublishersVM;
            }
            else
                return null;
            

            /*
            
            // Kraci zapis
             
            var returnedPublishersVM = _mapper.Map<List<PublisherVM>>(_context.Publishers.ToList());

            if (returnedPublishersVM is not null)
                return returnedPublishersVM;
            else
                return null;

            */
        }
    }
}
