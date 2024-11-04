namespace RS.OrderService.ShoppingCarts.CreateCart
{
    public class CreateCartCommandArgs
    {
        public DateTime ExpiresDate { get; set; }
        public float Total { get; set; }
        public long TotalItem { get; set; }
    }
}
