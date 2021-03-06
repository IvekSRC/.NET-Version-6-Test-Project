using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Repository;
using NUnit.Framework;
using System;
using AutoMapper;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        IServiceCollection? _services;
        IServiceProvider? _serviceProvider;

        private PublishersService? _publisherService;

        [OneTimeSetUp]
        public void Setup()
        {
            _services = new ServiceCollection();
            _services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                "Data Source=(localdb)\\TestBaza;Initial Catalog=databaseV1;Integrated Security=True;Pooling=False"));

            _services.AddTransient<IPublisherRepo, PublisherRepo>();
            _services.AddTransient<PublishersService>();

            _services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            _serviceProvider = _services.BuildServiceProvider();

            _publisherService = (PublishersService?)_serviceProvider.GetService(typeof(PublishersService));
        }

        [Test, Order(1)]
        public void IsReturnedPublisher()
        {
            var result = _publisherService?.GetPublisherById(4);
            Assert.That(result, Is.Not.Null);
        }

        [Test, Order(2)]
        public void TestReturnedNameForPublisher()
        {
            var result = _publisherService?.GetPublisherById(4);
            Assert.That(result?.Name, Is.EqualTo("Ivan Petkovic"));
        }

        [Test, Order(3)]
        public void TestCountReturnedPublishers()
        {
            var result = _publisherService.GetAllPublisher();
            Assert.That(result.Count, Is.EqualTo(7));
        }
    }
}