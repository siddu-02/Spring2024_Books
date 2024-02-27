using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;

namespace Spring2024_Books.Controllers
{
    public class BookController : Controller
    {
        private BooksDBContext _dbContext;
        //private IWebHostEnvironment environment;

        public BookController(BooksDBContext dbContext)
        {
            _dbContext = dbContext;
            //environment = environment;
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
                IEnumerable<SelectListItem> listOfCategories = _dbContext.Categories.ToList().Select(u => new SelectListItem

                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()

                });

                ViewBag.LisOfCategories = listOfCategories;
                return View();

            }
            [HttpPost]
            public IActionResult Create ()
            {
                
               /* if(ModelState.IsValid)
                {
                    string wwwrootPath = environment.WebRootPath;
                    if(imgFile != null)
                    {
                        using (var fileStream = new FileStream(Path.Combine(wwwrootPath, @"Images\BookImages\"+ imgFile.FileName),FileMode.Create))
                                {
                            imgFile.CopyTo(fileStream);
                        }


                    }*/
                    ViewBag.ListOfCategories = listOfCategories;

                    _dbContext.Books.Add();
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index", "Book");

                }

                return View(bookObj);
            }
            [HttpGet]
            public IActionResult Edit(Book bookObj)
            { return View(bookObj); }





        }
    }
    }
