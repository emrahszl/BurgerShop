using BurgerShop.Data.Enums;

namespace BurgerShop.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public OrderSize OrderSize { get; set; }

        public int OrderCount { get; set; }

        public string BurgerShopUserId { get; set; } = null!;



        public List<int> MenuIds { get; set; } = new();

        public List<int> ExtraMaterialIds { get; set; } = new();
    }
}
