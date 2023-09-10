using BurgerShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurgerShop.Areas.Manager.Controllers
{
    public class ManagementController : ManagerBaseController
    {
        private readonly BurgerShopDbContext _db;
        private readonly UserManager<BurgerShopUser> _userManager;

        public ManagementController(BurgerShopDbContext db, UserManager<BurgerShopUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var customers = _db.Users.Include(u => u.Orders).ThenInclude(o => o.Menus).Include(u => u.Orders).ThenInclude(o => o.ExtraMaterials).Where(u => u.Email != "manager@manager.com" && u.Orders.Count > 0).ToList();

            ViewBag.CustomerCount = _db.Users.Count() - 1;
            ViewBag.OrderCount = _db.Orders.Count();

            return View(customers);
        }
    }
}
