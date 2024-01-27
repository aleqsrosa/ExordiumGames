using ExordiumGames.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExordiumGames.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserVM> User { get; set; }
        public DbSet<RetailerVM> Retailer { get; set; }
        public DbSet<CategoryVM> Category { get; set; }
        public DbSet<ItemsVM> Items { get; set; }
    }
}
