using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;
using Spring2024_Books.Models.ViewModels;

namespace Spring2024_Books.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private BooksDBContext _dbContext;

        public CartController(BooksDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItemsList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Book);
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM
            {
                CartItems = cartItemsList,

                Order = new Order()
            };

            foreach (var cartItem in shoppingCartVM.CartItems)
            {
                //this is subtotal of individual product
                cartItem.Subtotal = cartItem.Book.Price * cartItem.Quantity;

                //this is order total
                shoppingCartVM.Order.OrderTotal += cartItem.Subtotal;
            }

            return View(shoppingCartVM);
        }
        public IActionResult IncrementQtyByOne(int id)
        {
            Cart cartObj = _dbContext.Carts.Find(id);

            cartObj.Quantity += 1;
            _dbContext.Update(cartObj);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult DecrementQtyByOne(int id)
        {
            Cart cartobj = _dbContext.Carts.Find(id);

            if (cartobj.Quantity > 1)
            {
                cartobj.Quantity -= 1;
                _dbContext.Carts.Update(cartobj);
                _dbContext.SaveChanges();
            }

            else
            {
                _dbContext.Carts.Remove(cartobj);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");

        }
        public IActionResult RemoveFromCart(int id)
        {
            Cart cartobj = _dbContext.Carts.Find(id);

            _dbContext.Carts.Remove(cartobj);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ReviewOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItemsList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Book);

            ShoppingCartVM shoppingCartVM = new ShoppingCartVM
            {
                CartItems = cartItemsList,
                Order = new Order()
            };

            foreach (var cartItem in shoppingCartVM.CartItems)
            {
                cartItem.Subtotal = cartItem.Book.Price * cartItem.Quantity;

                shoppingCartVM.Order.OrderTotal += cartItem.Subtotal;
            }

            shoppingCartVM.Order.ApplicationUser = _dbContext.ApplicationUsers.Find(userId);
            shoppingCartVM.Order.CustomerName = shoppingCartVM.Order.ApplicationUser.Name;
            shoppingCartVM.Order.StreetAddress = shoppingCartVM.Order.ApplicationUser.StreetAddress;
            shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
            shoppingCartVM.Order.State = shoppingCartVM.Order.ApplicationUser.State;
            shoppingCartVM.Order.PostalCode = shoppingCartVM.Order.ApplicationUser.PostalCode;
            shoppingCartVM.Order.Phone = shoppingCartVM.Order.ApplicationUser.PhoneNumber;

            return View(shoppingCartVM);

        }
        [HttpPost]
        [ActionName("ReviewOrder")]
        public IActionResult ReviewOrderPOST(ShoppingCartVM shoppingCartVM)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItemsList = _dbContext.Carts.Where(c => c.UserId == userId).Include(c => c.Book);

            shoppingCartVM.CartItems = cartItemsList;

            foreach (var cartItem in shoppingCartVM.CartItems)
            {
                cartItem.Subtotal = cartItem.Book.Price * cartItem.Quantity;

                shoppingCartVM.Order.OrderTotal += cartItem.Subtotal;
            }
            shoppingCartVM.Order.ApplicationUser = _dbContext.ApplicationUsers.Find(userId);
            shoppingCartVM.Order.CustomerName = shoppingCartVM.Order.ApplicationUser.Name;
            shoppingCartVM.Order.StreetAddress = shoppingCartVM.Order.ApplicationUser.StreetAddress;
            shoppingCartVM.Order.City = shoppingCartVM.Order.ApplicationUser.City;
            shoppingCartVM.Order.State = shoppingCartVM.Order.State;
            shoppingCartVM.Order.PostalCode = shoppingCartVM.Order.ApplicationUser.PostalCode;
            shoppingCartVM.Order.Phone = shoppingCartVM.Order.Phone;

            shoppingCartVM.Order.OrderDate = DateOnly.FromDateTime(DateTime.Now);
            shoppingCartVM.Order.OrderStatus = "Pending";
            shoppingCartVM.Order.PaymentStatus = "Pending";

             _dbContext.Orders.Add(shoppingCartVM.Order);
            _dbContext.SaveChanges();

            foreach (var cartItem in shoppingCartVM.CartItems)
            {
                OrderDetail orderDetail = new()
                {
                    OrderId = shoppingCartVM.Order.OrderId,
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Book.Price
                };
                _dbContext.OrderDetails.Add(orderDetail);
            }
            _dbContext.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { id = shoppingCartVM.Order.OrderId });

        }
        public IActionResult OrderConfirmation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Cart> listofCartItems = _dbContext.Carts.ToList().Where(c => c.UserId == userId).ToList();

            _dbContext.Carts.RemoveRange(listofCartItems);
            _dbContext.SaveChanges();


            return View(id);
        }

    }
}

