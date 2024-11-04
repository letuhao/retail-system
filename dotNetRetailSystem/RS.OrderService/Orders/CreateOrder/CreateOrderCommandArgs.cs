namespace RS.OrderService.Orders.CreateOrder
{
    public class CreateOrderCommandArgs
    {
        public string OrderNumber { get; set; } = default!;
        public int Status { get; set; }
        public float TotalAmount { get; set; }
        public float SubTotal { get; set; }
        public float Tax { get; set; }
        public float ShippingCost { get; set; }
        public float Discount { get; set; }
        public string ShippingAddress { get; set; } = default!;
        public string BillingAddress { get; set; } = default!;
        public int PaymentMethodId { get; set; }
        public int PaymentStatus { get; set; }
        public string Note { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public Guid ShopId { get; set; } = default!;
    }
}
