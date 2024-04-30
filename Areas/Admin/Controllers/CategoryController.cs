using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;

namespace Spring2024_Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    //[Area("Customer")]
    //[Authorize(Roles = "Customer")]
    public class CategoryController : Controller

    {
        private BooksDBContext _dbContext;
        public CategoryController(BooksDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var listOfCategories = _dbContext.Categories.ToList();

            return View(listOfCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category categoryObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(categoryObj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Category");
            }

            return View(categoryObj);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = _dbContext.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("CategoryId,Name,Description")] Category categoryObj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(categoryObj);
                _dbContext.SaveChanges(true);

                return RedirectToAction("Index", "Category");
            }

            return View(categoryObj);
        }
        [HttpGet]
        //id comes from URL

        public IActionResult Delete(int id)
        {
            //
            Category category = _dbContext.Categories.Find(id);

            //THe details of the category is displayed and the option to delete
            return View(category);

        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            Category categoryObj = _dbContext.Categories.Find(id);

            _dbContext.Categories.Remove(categoryObj);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Category");

        }

        public IActionResult Details(int id)
        {
            Category categoryObj = _dbContext.Categories.Find(id);

            return View(categoryObj);
        }
    }
}
