using Assignment_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_BE.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
