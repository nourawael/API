using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Contexts
{
    public class StoreIdenitityDbContext : IdentityDbContext<AppUser>
    {
        public StoreIdenitityDbContext(DbContextOptions<StoreIdenitityDbContext> options) : base(options)
        {
        }
    }
}
