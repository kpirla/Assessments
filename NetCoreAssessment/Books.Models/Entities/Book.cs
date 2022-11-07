using System.ComponentModel.DataAnnotations;

namespace Books.Models.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string Edition { get; set; }
        public string Place { get; set; }
        public string PageNumbers { get; set; }
    }
}
