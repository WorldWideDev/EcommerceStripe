using Microsoft.EntityFrameworkCore;
namespace ECommerce.Models
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options){}
        public DbSet<Customer> customers {get;set;}
        public DbSet<Product> products {get;set;}
        public DbSet<Order> orders {get;set;}
    }
}