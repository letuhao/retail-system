namespace RS.OrderService.CartItems.CreateCartItem
{
    public class CreateCartItemCommandArgs
    {
        public long Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float SubTotal { get; set; }
        public float Total { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
