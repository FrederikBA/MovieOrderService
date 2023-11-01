using System.ComponentModel.DataAnnotations;

namespace MovieOrders.Web.Model;

public class Order
{
    [Key]
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderLine> OrderLines { get; set; } = new();
}