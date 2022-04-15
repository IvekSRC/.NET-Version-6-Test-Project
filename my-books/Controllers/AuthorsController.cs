using Catel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Auth;
using my_books.Data.Models.ViewModels;
using my_books.Data.Services;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        //[Authorize]
        [HttpPost("add-author")]
        [ValidateModelAttribte]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            if(ModelState.IsValid)
            {
                _authorService.AddAuthor(author);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var response = _authorService.GetAuthorWithBooks(id);
            return Ok(response);
        }
    }
}
