using Books.DTO.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services.Interfaces
{
    public interface IBookInterface
    {
        public Task<List<BookResp>> BookListByPAT();
        public Task<List<BookResp>> BookListByAT();
        public Task<decimal> TotalPrice();
        public Task<List<BookResp>> BookListByPATSP();
        public Task<List<BookResp>> BookListByATSP();
        public Task<bool> InsertBookList(List<BookResp> inBookList);
    }
}
