using BurgerShop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string BurgerShopUserId { get; set; } = null!;

        public OrderSize OrderSize { get; set; }

        public int OrderCount { get; set; }



        public BurgerShopUser BurgerShopUser { get; set; } = null!;

        public List<Menu> Menus { get; set; } = new();

        public List<ExtraMaterial> ExtraMaterials { get; set; } = new();
    }
}
