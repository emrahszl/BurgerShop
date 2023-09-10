using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Data.Entities
{
    public class ExtraMaterial
    {
        public int Id { get; set; }

        public string MaterialName { get; set; } = null!;

        public decimal MaterialPrice { get; set; }

        public string ExtraNamePrice => MaterialName + " " + MaterialPrice + " TL";


        public List<Order> Orders { get; set; } = new();
    }
}
