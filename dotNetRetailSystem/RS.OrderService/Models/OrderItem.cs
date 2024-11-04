namespace RS.OrderService.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public string ProductName { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Guid ProductId { get; set; } = default!;
    }
}
