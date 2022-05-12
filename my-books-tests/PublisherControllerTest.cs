using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using my_books.Controllers;
using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Data.Services;
using my_books.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_books_tests
{
    public class PublisherControllerTest
    {
        private IServiceCollection _services;
        private IServiceProvider _serviceProvider;

        private PublisherController? _publisherController;

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

            _publisherController = new PublisherController((PublishersService)_serviceProvider.GetService(typeof(PublishersService)));
        }

        [Test, Order(1)]
        public void TestGetPublisherAction()
        {
            var actionResult = _publisherController.GetPublisherById(4);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        }

        [Test, Order(2)]
        public void TestDeletePublisherAction()
        {
            var actionResult = _publisherController.DeletePublisherId(4);
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }
    }
}
