using Books.DTO.Book;
using Books.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration iConfig;
        public readonly IBookInterface iBookInterface;
        public HomeController(IConfiguration iConfig, IBookInterface iBookInterface)
        {
            this.iConfig = iConfig;
            this.iBookInterface = iBookInterface;
        }

        public async Task<IActionResult> Index()
        {
            //List<BookResp> inBookList = new List<BookResp>();
            //inBookList = iConfig.GetSection("BookResp").Get<List<BookResp>>();
            //bool respFlg = await iBookInterface.InsertBookList(inBookList);
            await Task.Delay(0);
            return View();
        }
    }
}
