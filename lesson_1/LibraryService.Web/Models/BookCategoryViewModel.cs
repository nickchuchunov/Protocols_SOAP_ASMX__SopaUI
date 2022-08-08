namespace LibraryService.Web.Models
{
    using LibraryServiceReference1;
    public class BookCategoryViewModel
    {
        public Book[] Books { get; set; }
        public SearchType SearchType { get; set; }
        public string? SearchString { get; set; }

    }
}
