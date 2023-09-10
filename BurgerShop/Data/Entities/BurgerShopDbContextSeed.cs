using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerShop.Data.Entities
{
    public static class BurgerShopDbContextSeed
    {
        public static async Task SeedAsync(BurgerShopDbContext db, RoleManager<IdentityRole> roleManager, UserManager<BurgerShopUser> userManager)
        {
            var managerEmail = "manager@manager.com";
            var managerPassword = "M@nager1";

            List<string> roles = new List<string>() { "Manager", "Customer" };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                    continue;

                await roleManager.CreateAsync(new IdentityRole(role));
            }

            var managerUser = new BurgerShopUser()
            {
                UserName = managerEmail,
                FirstName = "Manager",
                LastName = string.Empty,
                Email = managerEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(managerUser, managerPassword);
            await userManager.AddToRoleAsync(managerUser, roles[0]);

            if (db.Menus.Count() == 0)
            {
                List<Menu> menus = new List<Menu>
                    {
                        new Menu()
                        {
                            MenuName = "Chili Burger",
                            MenuPrice = 120
                        },
                        new Menu()
                        {
                            MenuName = "Classic Burger",
                            MenuPrice = 150
                        },
                        new Menu()
                        {
                            MenuName = "BBQ Burger",
                            MenuPrice = 175
                        },
                        new Menu()
                        {
                            MenuName = "Big Daddy Burger",
                            MenuPrice = 200
                        },
                        new Menu()
                        {
                            MenuName = "Triple Mustang Burger",
                            MenuPrice = 475
                        }
                    };

                db.AddRange(menus);
                db.SaveChanges();
            }



            if (db.ExtraMaterials.Count() == 0)
            {
                List<ExtraMaterial> extraMaterials = new List<ExtraMaterial>
                {
                    new ExtraMaterial()
                    {
                        MaterialName = "Ketchup",
                        MaterialPrice = 2.5m
                    },
                    new ExtraMaterial()
                    {
                        MaterialName = "Mayonnaise",
                        MaterialPrice = 2.5m
                    },
                    new ExtraMaterial()
                    {
                        MaterialName = "BBQ",
                        MaterialPrice = 4
                    },
                    new ExtraMaterial()
                    {
                        MaterialName = "Mustard",
                        MaterialPrice = 4
                    },
                    new ExtraMaterial()
                    {
                        MaterialName = "Ranch",
                        MaterialPrice = 3.5m
                    },
                    new ExtraMaterial()
                    {
                        MaterialName = "French Fries",
                        MaterialPrice = 45
                    },
                    new ExtraMaterial()
                    {
                        MaterialName = "Onion Rings",
                        MaterialPrice = 55
                    },
                };

                db.AddRange(extraMaterials);
                db.SaveChanges();
            }
        }
    }
}
