using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL
{
    public class PetShopContext: DbContext
    {
        public PetShopContext(DbContextOptions opt): base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PreviousOwner)
                .WithMany(o => o.Pets).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PetColour>().HasKey(pc => new { pc.PetId, pc.ColourId });
            modelBuilder.Entity<PetColour>().HasOne(pc => pc.Pet)
                .WithMany(p => p.Colours);
            modelBuilder.Entity<PetColour>().HasOne(pc => pc.Colour)
                .WithMany(c => c.PetList);

        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PetColour> PetColour { get; set; }
        public DbSet<Colour> Colours { get; set; }
    }
}