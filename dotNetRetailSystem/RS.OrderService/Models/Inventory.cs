namespace RS.OrderService.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public long Quantity { get; set; }
        public float UnitPrice { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }
    }
}
