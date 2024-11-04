namespace RS.OrderService.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public DateTime ExpiresDate { get; set; }
        public float Total { get; set; }
        public long TotalItem { get; set; }
    }
}
