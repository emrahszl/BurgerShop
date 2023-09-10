using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerShop.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BurgerShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BurgerShopUser class
public class BurgerShopUser : IdentityUser
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public List<Order> Orders { get; set; } = new();
}

