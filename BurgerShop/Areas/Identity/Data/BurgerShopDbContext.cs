using BurgerShop.Areas.Identity.Data;
using BurgerShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BurgerShop.Data;

public class BurgerShopDbContext : IdentityDbContext<BurgerShopUser>
{
    public BurgerShopDbContext(DbContextOptions<BurgerShopDbContext> options)
        : base(options)
    {

    }


    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Menu> Menus => Set<Menu>();

    public DbSet<ExtraMaterial> ExtraMaterials => Set<ExtraMaterial>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
