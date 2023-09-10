using BurgerShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace BurgerShop.Areas.Manager.Controllers
{
    public class ExtraMaterialsController : ManagerBaseController
    {
        private readonly BurgerShopDbContext _db;

        public ExtraMaterialsController(BurgerShopDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ExtraMaterials.ToList());
        }

        public IActionResult CreateMaterial()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateMaterial(ExtraMaterial extraMaterial)
        {
            if (extraMaterial == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _db.ExtraMaterials.Add(extraMaterial);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteMaterial(int id)
        {
            return View(_db.ExtraMaterials.Find(id));
        }

        [ActionName("DeleteMaterial")]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteMaterialConfirm(int id)
        {
            var extraMaterial = _db.ExtraMaterials.Find(id);

            if (extraMaterial == null)
                return NotFound();

            _db.ExtraMaterials.Remove(extraMaterial);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult EditMaterial(int id)
        {
            return View(_db.ExtraMaterials.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditMaterial(ExtraMaterial extraMaterial)
        {
            if (extraMaterial == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _db.ExtraMaterials.Update(extraMaterial);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
