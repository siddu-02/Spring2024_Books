using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;

namespace Spring2024_Books.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]


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
            var listOfBooks = _dbContext.Books.Include(c => c.category);

            return View(listOfBooks.ToList());

        }
        [HttpGet]

        public IActionResult Details(int id)
        {
            Book book = _dbContext.Books.Find(id);

            _dbContext.Books.Entry(book).Reference(b => b.category).Load();

            
             var cart = new Cart
             {
                 BookId = id,
                 Book = book,
                 Quantity = 1
             };
            return View(cart);

        }

        [HttpPost]
        [Authorize]

        public IActionResult Details(Cart cart)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            cart.UserId = userId; 

            Cart existingCart = _dbContext.Carts.FirstOrDefault(c => c.UserId == userId && c.BookId == cart.BookId);

            if (existingCart != null) 
            {
                existingCart.Quantity += cart.Quantity;
                _dbContext.Carts.Update(existingCart);
            }
            else
            {
                _dbContext.Carts.Add(cart); 
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
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