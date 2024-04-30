using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spring2024_Books.Data;

namespace Spring2024_Books.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class UserController : Controller
    {
        private BooksDBContext _dbContext;

        private UserManager<IdentityUser> _userManager;

        public UserController(BooksDBContext dbContext, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            List<ApplicationUser> userList = _dbContext.ApplicationUsers.ToList();

            var allRoles = _dbContext.Roles.ToList();
            var userRoles = _dbContext.UserRoles.ToList();

            foreach (var u in userList)
            {
                //find first user in the list, get me the role ID, i eventually want the role name
                var roleId = userRoles.Find(ur => ur.UserId == u.Id).RoleId;

                var roleName = allRoles.Find(r => r.Id == roleId).Name;

                u.RoleName = roleName;
            }


            return View(userList);
        }

        public IActionResult LockUnlock(string id)
        {

            var userFromDB = _dbContext.ApplicationUsers.Find(id);

            if(userFromDB.LockoutEnd != null&& userFromDB.LockoutEnd> DateTime.Now)
            {
                userFromDB.LockoutEnd = DateTime.Now.AddYears(10);

            }
            else
            {
                userFromDB.LockoutEnd = DateTime.Now.AddYears(10);


            }
            _dbContext.SaveChanges();

           return RedirectToAction("Index");
        }
        [HttpGet]

        public IActionResult EditUserRole(string id)
        {
            var currentUserRole = _dbContext.UserRoles.FirstOrDefault(ur => ur.UserId == id);


            IEnumerable<SelectListItem> listOfRoles = _dbContext.Roles.ToList().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            });


            ViewBag.ListOfRoles = listOfRoles;

            ViewBag.UserInfo = _dbContext.ApplicationUsers.Find(id);

            return View(currentUserRole);




        }

        [HttpPost]
        public IActionResult EditUserRole(Microsoft.AspNetCore.Identity.IdentityUserRole<string> updatedRole)
        {
            ApplicationUser applicationUser = _dbContext.ApplicationUsers.Find(updatedRole.UserId);

            string newRoleName = _dbContext.Roles.Find(updatedRole.RoleId).Name;

            string oldRoleId = _dbContext.UserRoles.FirstOrDefault(u => u.UserId == applicationUser.Id).RoleId;

            string oldRoleName = _dbContext.Roles.Find(oldRoleId).Name;

            _userManager.RemoveFromRoleAsync(applicationUser, oldRoleName).GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(applicationUser, oldRoleName).GetAwaiter().GetResult();

            return RedirectToAction("Index");


        }



    }
}
