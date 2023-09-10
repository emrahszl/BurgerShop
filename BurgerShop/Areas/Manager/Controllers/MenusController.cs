using BurgerShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace BurgerShop.Areas.Manager.Controllers
{
    public class MenusController : ManagerBaseController
    {
        private readonly BurgerShopDbContext _db;

        public MenusController(BurgerShopDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Menus.ToList());
        }

        public IActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateMenu(Menu menu)
        {
            if (menu == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _db.Menus.Add(menu);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteMenu(int id) 
        {
            return View(_db.Menus.Find(id));
        }

        [ActionName("DeleteMenu")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteMenuConfirm(int id)
        {
            var menu = _db.Menus.Find(id);

            if (menu == null)
                return NotFound();

            _db.Menus.Remove(menu);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditMenu(int id)
        {
            return View(_db.Menus.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditMenu(Menu menu)
        {
            if (menu == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _db.Menus.Update(menu);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
