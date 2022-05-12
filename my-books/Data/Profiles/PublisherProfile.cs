using AutoMapper;
using my_books.Data.Models;
using my_books.Data.Models.ViewModels;

namespace my_books.Data.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherVM, Publisher>().ReverseMap();
        }
    }
}
