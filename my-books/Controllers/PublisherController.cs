using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_books.Auth;
using my_books.Data.Models.ViewModels;
using my_books.Data.Services;
using my_books.Exceptions;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private PublishersService _publisherService;

        public PublisherController(PublishersService publisherService)
        {
            _publisherService = publisherService;
        }

        [Authorize(Permissions.Users.Create)]
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Permissions.Users.View)]
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {            
            var _response = _publisherService.GetPublisherById(id);

            if(_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Permissions.Users.View)]
        [HttpGet("get-publisher-books-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publisherService.GetPublisherData(id);
            return Ok(_response);
        }

        [Authorize(Permissions.Users.Delete)]
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherId(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Permissions.Users.Edit)]
        [HttpPost("return-publisher-by-id/{id}")]
        public IActionResult ReturnDeletedPublisher(int id)
        {
            try
            {
                _publisherService.ReturnDeletedPublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
