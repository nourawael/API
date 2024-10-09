using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager) 
        {
            if (!userManager.Users.Any()) 
            {
                var user = new AppUser
                {
                    DipslayName="Noura Wael",
                    Email="noura@gmail.com",
                    UserName="Noura",
                    Address= new Address
                    {
                        FirstName="Noura",
                        LastName="Wael",
                        City="Obour",
                        State="Cairo",
                        Streeet="3",
                        PostalCode="23456"
                    }
                };

                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
}
