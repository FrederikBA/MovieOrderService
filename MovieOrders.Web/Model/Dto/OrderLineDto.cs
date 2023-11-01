namespace MovieOrders.Web.Model.Dto;

public class OrderLineDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; } = string.Empty;
}