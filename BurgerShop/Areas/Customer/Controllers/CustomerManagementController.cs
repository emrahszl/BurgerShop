using BurgerShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerShop.Areas.Customer.Controllers
{
    public class CustomerManagementController : CustomerBaseController
    {
        private readonly BurgerShopDbContext _db;
        private readonly UserManager<BurgerShopUser> _userManager;

        public CustomerManagementController(BurgerShopDbContext db, UserManager<BurgerShopUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var customerId = _userManager.GetUserId(User);
            var customer = _db.Users.Find(customerId);
            var orders = _db.Orders.Where(o => o.BurgerShopUserId == customerId).Include(o => o.Menus).Include(o => o.ExtraMaterials).ToList();

            ViewBag.CustomerName = $"{customer!.FirstName} {customer.LastName}";

            return View(orders);
        }
    }
}
