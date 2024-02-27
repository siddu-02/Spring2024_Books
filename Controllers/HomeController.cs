using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;

namespace Spring2024_Books.Controllers
{
    public class HomeController : Controller
    {
        private BooksDBContext _dbContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BooksDBContext booksDBContext)
        {

            _logger = logger;
            _dbContext = booksDBContext;
        }
        
        public IActionResult Index()
        {
            var listOfBooks = _dbContext.Books.Include(c => c.CategoryId);
            return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}