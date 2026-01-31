namespace OrderSevice.DTOs
{
    public class OrderDto
    {
        public string OrderNumber { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
    }
}
