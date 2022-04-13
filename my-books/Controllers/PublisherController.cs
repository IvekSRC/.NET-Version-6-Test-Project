using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResult;
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

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");
            
            var _response = _publisherService.GetPublisherById(id);

            if(_response != null)
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};

                //return new CustomActionResult(_responseObj);
                return Ok(_response);
            }
            else
            {
                //var _responseObj = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is comming from publishers controller")
                //};

                //return new CustomActionResult(_responseObj);
                return NotFound();
            }
        }

        [HttpGet("get-publisher-books-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publisherService.GetPublisherData(id);
            return Ok(_response);
        }

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
    }
}
