using Microsoft.EntityFrameworkCore;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL
{
    public class PetShopContext: DbContext
    {
        public PetShopContext(DbContextOptions opt): base(opt)
        {

        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owner { get; set; }
    }
}