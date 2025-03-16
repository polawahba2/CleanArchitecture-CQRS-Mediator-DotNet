using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Identity;
namespace Infrastructure
{
       public class ApplicationDBContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        // public DbSet<Product> Products { get; set; }
        // public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
    }
}