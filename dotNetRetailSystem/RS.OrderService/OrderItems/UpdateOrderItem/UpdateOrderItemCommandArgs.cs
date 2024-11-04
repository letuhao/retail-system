namespace RS.OrderService.OrderItems.UpdateOrderItem
{
    public class UpdateOrderItemCommandArgs
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public string ProductName { get; set; } = default!;
    }
}
