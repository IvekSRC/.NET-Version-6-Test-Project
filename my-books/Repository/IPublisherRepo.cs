using my_books.Data.Models;
using my_books.Data.Models.ViewModels;

namespace my_books.Repository
{
    public interface IPublisherRepo
    {
        Publisher AddPublisher(PublisherVM publisher);
        Publisher? GetPublisherById(int id);
        PublisherWithBooksAndAuthorsVM? GetPublisherData(int publisherId);
        Boolean DeletePublisherById(int publisherId);

    }
}
