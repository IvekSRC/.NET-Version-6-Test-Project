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
        Boolean ReturnDeletedPublisher(int publisherId);
        List<Publisher> TestFullSearch(string forSearch);
        List<PublisherVM>? GetAllPublisher();
    }
}
