using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spring2024_Books.Data;
using Spring2024_Books.Models;
using Spring2024_Books.Models.ViewModels;

namespace Spring2024_Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    //[Area("Customer")]
    //[Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private BooksDBContext _dbContext;

        [BindProperty]
        public OrderVM orderVM { get; set; }

        public OrderController(BooksDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Order> listOfOrders = _dbContext.Orders.Include(o => o.ApplicationUser).ToList();

            return View(listOfOrders);
        }
        public IActionResult Details(int id)

        {
            Order order = _dbContext.Orders.Find(id);

            IEnumerable<OrderDetail> orderDetails = _dbContext.OrderDetails.Where(o => o.OrderId == id).Include(od => od.Book);

            OrderVM orderVM = new OrderVM

            {
                Order = order,
                OrderDetails = orderDetails

            };
            return View(orderVM);

        }
        [HttpPost]
        public IActionResult UpdateOrderInformation()
        {
            Order orderFromDB = _dbContext.Orders.Find(orderVM.Order.OrderId);

            orderFromDB.CustomerName = orderVM.Order.CustomerName;
            orderFromDB.StreetAddress = orderVM.Order.StreetAddress;
            orderFromDB.City = orderVM.Order.City;
            orderFromDB.State = orderVM.Order.State;
            orderFromDB.PostalCode = orderVM.Order.PostalCode;
            orderFromDB.Phone = orderVM.Order.Phone;

            // if (!string.IsNullOrEmpty(orderVM.Order.ShippingDate.ToString()))
            orderFromDB.ShippingDate = orderVM.Order.ShippingDate;

            //  if (!string.IsNullOrEmpty(orderVM.Order.TrackingNumber))
            orderFromDB.TrackingNumber = orderVM.Order.TrackingNumber;

            //  if (!string.IsNullOrEmpty(orderVM.Order.Carrier))
            orderFromDB.Carrier = orderVM.Order.Carrier;

            orderFromDB.OrderStatus = orderVM.Order.OrderStatus;

            _dbContext.Orders.Update(orderFromDB);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = orderFromDB.OrderId });
        }

        public IActionResult ProcessOrder()
        {
            Order order = _dbContext.Orders.Find(orderVM.Order.OrderId);

            order.ShippingDate = DateOnly.FromDateTime(DateTime.Now).AddDays(7);

            order.OrderStatus = "Processing";

            order.Carrier = "USPS";

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            return RedirectToAction("Details", new { id = order.OrderId });



        }

        public IActionResult CompleteOrder()

        {
            Order order = _dbContext.Orders.Find(orderVM.Order.OrderId);

            order.OrderStatus = "Order Complete";


            order.ShippingDate = DateOnly.FromDateTime(DateTime.Now);

            _dbContext.Orders.Update(order);

            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = order.OrderId });

        }





    }
}
