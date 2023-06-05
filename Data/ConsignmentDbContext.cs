using CsdsShop.Models;
using Microsoft.EntityFrameworkCore;
namespace CsdsShop.Data

{
    public class ConsignmentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = "Data Source=consignmentdatabase.db";
            optionsBuilder.UseSqlite(connectionString);
        }

        // DbSet properties for your entities
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
