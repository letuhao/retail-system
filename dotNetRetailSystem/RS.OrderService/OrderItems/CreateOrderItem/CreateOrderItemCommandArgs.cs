namespace RS.OrderService.OrderItems.CreateOrderItem
{
    public class CreateOrderItemCommandArgs
    {
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public string ProductName { get; set; } = default!;
        public Guid OrderId { get; set; } = default!;
        public Guid ProductId { get; set; } = default!;
    }
}
