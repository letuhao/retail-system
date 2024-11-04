namespace RS.OrderService.CartItems.UpdateCartItem
{
    public class UpdateCartItemCommandArgs
    {
        public Guid Id { get; set; }
        public long Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float SubTotal { get; set; }
        public float Total { get; set; }
        public Guid ProductId { get; set; }
    }
}
