using Books.DTO.Book;
using Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration iConfig;
        public readonly IBookInterface iBookInterface;
        public BookController(IConfiguration iConfig, IBookInterface iBookInterface)
        {
            this.iConfig = iConfig;
            this.iBookInterface = iBookInterface;
        }
        public async Task<IActionResult> BookListByPAT()
        {
            List<BookResp> inBookList = new List<BookResp>();
            inBookList = await iBookInterface.BookListByPAT();
            return View("~/Views/Shared/_DataGrid.cshtml", inBookList);
        }
        public async Task<IActionResult> BookListByAT()
        {
            List<BookResp> inBookList = new List<BookResp>();
            inBookList = await iBookInterface.BookListByAT();
            return View("~/Views/Shared/_DataGrid.cshtml", inBookList);
        }
        public async Task<IActionResult> BookListByPATSP()
        {
            List<BookResp> inBookList = new List<BookResp>();
            inBookList = await iBookInterface.BookListByPATSP();
            return View("~/Views/Shared/_DataGrid.cshtml", inBookList);
        }
        public async Task<IActionResult> BookListByATSP()
        {
            List<BookResp> inBookList = new List<BookResp>();
            inBookList = await iBookInterface.BookListByATSP();
            return View("~/Views/Shared/_DataGrid.cshtml", inBookList);
        }
        public async Task<IActionResult> TotalPrice()
        {
            decimal totalPrice = 0;
            totalPrice = await iBookInterface.TotalPrice();
            ViewBag.Message = "Total price of all books in the database is: " + totalPrice.ToString();
            return View("~/Views/Shared/_Message.cshtml");
        }
        public async Task<IActionResult> InsertBookList()
        {
            List<BookResp> inBookList = new List<BookResp>();
            inBookList = iConfig.GetSection("BookResp").Get<List<BookResp>>();
            bool respFlg = await iBookInterface.InsertBookList(inBookList);
            ViewBag.Message = "Data inserted successfully";
            return View("~/Views/Shared/_Message.cshtml");
        }
    }
}
