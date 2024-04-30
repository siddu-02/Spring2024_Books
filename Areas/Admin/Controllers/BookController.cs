using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;
using Spring2024_Books.Models.ViewModels;

namespace Spring2024_Books.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    //Area("Customer")]
    //[Authorize(Roles = "Customer")]
    public class BookController : Controller
    {
        private BooksDBContext _dbContext;
        private IWebHostEnvironment _environment;

        public BookController(BooksDBContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }


        public IActionResult Index()
        {
            {
                var listOfBooks = _dbContext.Books.ToList();

                return View(listOfBooks);

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> listOfCategories = _dbContext.Categories.ToList().Select(o =>
            new SelectListItem
            {
                Text = o.Name,
                Value = o.CategoryId.ToString()
            });
            //ViewBag.ListOfCategories = listOfCategories;

            BookwithCategoriesVM bookWithCategoriesVM = new BookwithCategoriesVM();
            bookWithCategoriesVM.Book = new Book();
            bookWithCategoriesVM.ListOfCategories = listOfCategories;
            return View(bookWithCategoriesVM);
        }

        [HttpPost]
        public IActionResult Create(BookwithCategoriesVM bookWithCategoriesVM, IFormFile ImgFile)
        {
            if (ModelState.IsValid)
            {
                string wwwrootpath = _environment.WebRootPath;
                if (ImgFile != null)
                {
                    //save to 
                    using (var fileStream = new FileStream(Path.Combine(wwwrootpath, @"Images\BookImages\" + ImgFile.FileName), FileMode.Create))
                    {
                        ImgFile.CopyTo(fileStream); //Saves file in path 
                    }

                    bookWithCategoriesVM.Book.ImgUrl = @"\Images\BookImages\" + ImgFile.FileName;
                }

                _dbContext.Books.Add(bookWithCategoriesVM.Book);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            return View(bookWithCategoriesVM);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Book book = _dbContext.Books.Find(id);  
            IEnumerable<SelectListItem> listOfCategories = _dbContext.Categories.ToList().Select(o =>
           new SelectListItem
           {
               Text = o.Name,
               Value = o.CategoryId.ToString()
           });
            BookwithCategoriesVM bookwithCategoriesVM = new BookwithCategoriesVM();
            bookwithCategoriesVM.Book = book;
            return View(bookwithCategoriesVM);


            //View Bag
            //ViewBag.ListOfCategories = listOfCategories;

            //Book bookObj = _dbContext.Books.Find(id);
            //return View(bookObj);
        }
        public IActionResult Edit(BookwithCategoriesVM bookwithCategoriesVM, IFormFile? ImgFile)
        {
            string wwwrootPath = _environment.WebRootPath;
            if (ModelState.IsValid)
            {
                if (ImgFile != null)
                {
                    if (!string.IsNullOrEmpty(bookwithCategoriesVM.Book.ImgUrl))
                    {
                        var oldImgPath = Path.Combine(wwwrootPath, bookwithCategoriesVM.Book.ImgUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(wwwrootPath, @"images\Book-Images\" + ImgFile.FileName), FileMode.Create))
                    {
                        ImgFile.CopyTo(fileStream); // Saves the file in the specified folder
                    }

                    bookwithCategoriesVM.Book.ImgUrl = @"\images\Book-Images\" + ImgFile.FileName; 
                }

                _dbContext.Books.Update(bookwithCategoriesVM.Book);

                _dbContext.SaveChanges();

                return RedirectToAction("Index");


            }

            return View(bookwithCategoriesVM);
        }
        public IActionResult Details(int id)
        {
            Book book = _dbContext.Books
                                   .Include(b => b.category) // Include the Category if you want to display category details
                                   .FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book book = _dbContext.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Book book = _dbContext.Books.Find(id);

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }



}

