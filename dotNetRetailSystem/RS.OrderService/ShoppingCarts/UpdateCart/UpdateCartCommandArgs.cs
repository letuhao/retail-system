namespace RS.OrderService.ShoppingCarts.UpdateCart
{
    public class UpdateCartCommandArgs
    {
        public Guid Id { get; set; }
        public DateTime ExpiresDate { get; set; }
        public float Total { get; set; }
        public long TotalItem { get; set; }
    }
}
