namespace OrderSevice.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
