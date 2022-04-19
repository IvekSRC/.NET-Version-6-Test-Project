namespace my_books.Paging
{
    public class PagingProperties
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public bool? SortOrder { get; set; }
        public string? SortCriterium { get; set; }
        public string? StartsWith { get; set; }
    }
}
