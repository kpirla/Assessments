using System.ComponentModel.DataAnnotations;

namespace Books.DTO.Book
{
    public class BookResp
    {
        [Key]
        public int BookId { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public string Author { get { return (AuthorLastName + " " + AuthorFirstName); } }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string Edition { get; set; }
        public string Place { get; set; }
        public string PageNumbers { get; set; }
        public string MLAStyleCitation { get { return ((AuthorLastName + ", " + AuthorFirstName) + " " + "\"" + Title + "\" " + Publisher + ", " + Year); } }
        public string ChicagoStyleCitation { get { return ((AuthorLastName + ", " + AuthorFirstName) + " " + "\"" + Title + "\", " + Edition + " (" + Place + ": " + Publisher + ", " + Year + "), " + PageNumbers); } }
    }
}
