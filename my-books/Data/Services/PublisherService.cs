using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Exceptions;
using my_books.Repository;
using System.Text.RegularExpressions;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private readonly IPublisherRepo _publisherRepo;

        public PublishersService(IPublisherRepo publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name))
                throw new PublisherNameException("Name starts with number", publisher.Name);            

            return _publisherRepo.AddPublisher(publisher);
        }

        public Publisher? GetPublisherById(int id)
        {
            return _publisherRepo.GetPublisherById(id);
        }

        public PublisherWithBooksAndAuthorsVM? GetPublisherData(int publisherId)
        {
            return _publisherRepo.GetPublisherData(publisherId);
        }

        public void DeletePublisherById(int id)
        {
            var isDeleted = _publisherRepo.DeletePublisherById(id);

            if (isDeleted == false)
                throw new Exception($"The publisher with id {id} does not exist");
        }

        public void ReturnDeletedPublisher(int publisherId)
        {
            var isReturned = _publisherRepo.ReturnDeletedPublisher(publisherId);

            if (isReturned == false)
                throw new Exception($"Not found.");
        }
        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
