using System.ComponentModel.DataAnnotations;

namespace my_books.Data.Models.ViewModels
{
    public class AuthorVM
    {
        [MinLength(1)]
        public string FullName { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
