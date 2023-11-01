namespace MovieOrders.Web.Model.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderLineDto> OrderLines { get; set; } = new();
}