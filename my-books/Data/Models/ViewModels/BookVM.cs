using System.ComponentModel.DataAnnotations;

namespace my_books.Data.Models.ViewModels
{
    public class BookVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }

        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
    }

    public class BookWithAuthorsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }

        public string PublisherName { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}
