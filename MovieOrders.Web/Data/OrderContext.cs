using Microsoft.EntityFrameworkCore;
using MovieOrders.Web.Model;

namespace MovieOrders.Web.Data;

public class OrderContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderLine> OrderLines { get; set; } = null!;
    
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable("Order")
            .HasMany(o => o.OrderLines)
            .WithOne(ol => ol.Order)
            .HasForeignKey(ol => ol.OrderId);
        
        modelBuilder.Entity<OrderLine>().ToTable("OrderLine");
    }
}