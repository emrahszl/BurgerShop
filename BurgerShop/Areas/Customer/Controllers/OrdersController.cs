using BurgerShop.Data;
using BurgerShop.Data.Entities;
using BurgerShop.Data.Enums;
using BurgerShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BurgerShop.Areas.Customer.Controllers
{
    public class OrdersController : CustomerBaseController
    {
        private readonly BurgerShopDbContext _db;
        private readonly UserManager<BurgerShopUser> _userManager;

        public OrdersController(BurgerShopDbContext db, UserManager<BurgerShopUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult CreateOrder()
        {
            ViewBag.OrderSizes = Enum.GetNames(typeof(OrderSize));
            ViewBag.Menus = _db.Menus.ToList();
            ViewBag.ExtraMaterials = _db.ExtraMaterials.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(OrderViewModel ovm)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ovm.BurgerShopUserId = userId!;

            //Kullanıcının seçtiği menü ve ekstra malzeme Id'lerini order nesnesinde tutun
            var selectedMenus = _db.Menus.Where(menu => ovm.MenuIds.Contains(menu.Id)).ToList();
            var selectedExtraMaterials = _db.ExtraMaterials.Where(extra => ovm.ExtraMaterialIds.Contains(extra.Id)).ToList();

            var order = new Order
            {
                OrderSize = ovm.OrderSize,
                OrderCount = ovm.OrderCount,
                Menus = selectedMenus,
                ExtraMaterials = selectedExtraMaterials,
                BurgerShopUserId = userId!
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            return RedirectToAction("Index","CustomerManagement");
        }

        public IActionResult DeleteOrder(int id)
        {
            return View(_db.Orders.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("DeleteOrder")]
        public IActionResult DeleteOrderConfirm(int id)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }

            return RedirectToAction("Index", "CustomerManagement");
        }

        public IActionResult EditOrder(int id)
        {
            var order = _db.Orders
             .Include(o => o.Menus)
             .Include(o => o.ExtraMaterials)
             .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderViewModel
            {
                Id = order.Id,
                OrderSize = order.OrderSize,
                OrderCount = order.OrderCount,
                BurgerShopUserId = _userManager.GetUserId(HttpContext.User)!,
                MenuIds = order.Menus.Select(menu => menu.Id).ToList(),
                ExtraMaterialIds = order.ExtraMaterials.Select(extra => extra.Id).ToList()
            };

            ViewBag.Menus = _db.Menus.ToList();
            ViewBag.ExtraMaterials = _db.ExtraMaterials.ToList();
            ViewBag.OrderSizes = Enum.GetNames(typeof(OrderSize));

            return View(viewModel);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditOrder(OrderViewModel ovm)
        {

            var order = _db.Orders
                .Include(o => o.Menus)
                .Include(o => o.ExtraMaterials)
                .FirstOrDefault(o => o.Id == ovm.Id);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderSize = ovm.OrderSize;
            order.OrderCount = ovm.OrderCount;

            var selectedMenus = _db.Menus.Where(menu => ovm.MenuIds.Contains(menu.Id)).ToList();
            var selectedExtraMaterials = _db.ExtraMaterials.Where(extra => ovm.ExtraMaterialIds.Contains(extra.Id)).ToList();

            order.Menus.Clear();
            order.ExtraMaterials.Clear();

            foreach (var menu in selectedMenus)
            {
                order.Menus.Add(menu);
            }

            foreach (var extra in selectedExtraMaterials)
            {
                order.ExtraMaterials.Add(extra);
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "CustomerManagement");
        }

        [HttpPost]
        public decimal CalculateOrderAmount(List<int> MenuIds, List<int> ExtraMaterialIds, OrderSize OrderSize, int OrderCount)
        {
            decimal menuPrice = 0m;
            decimal materialPrice = 0m;

            foreach (int menuId in MenuIds)
            {
                menuPrice += _db.Menus.Find(menuId)!.MenuPrice;
            }

            foreach (int emId in ExtraMaterialIds)
            {
                materialPrice += _db.ExtraMaterials.Find(emId)!.MaterialPrice;
            }

            var totalOrderAmount = (menuPrice + (OrderSize == OrderSize.Small ? 0m : OrderSize == OrderSize.Medium ? 25m : 50m) + materialPrice) * OrderCount;

            return totalOrderAmount;
        }
    }
}

