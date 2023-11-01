using System.ComponentModel.DataAnnotations;

namespace MovieOrders.Web.Model;

public class OrderLine
{
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
}