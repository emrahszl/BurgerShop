using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Data.Entities
{
    public class Menu
    {
        public int Id { get; set; }

        public string MenuName { get; set; } = null!;

        public decimal MenuPrice { get; set; }

        public string MenuNamePrice => MenuName + " " + MenuPrice + " TL";



        public List<Order> Orders { get; set; } = new();
    }
}
