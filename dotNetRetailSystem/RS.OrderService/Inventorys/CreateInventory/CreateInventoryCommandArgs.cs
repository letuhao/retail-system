namespace RS.OrderService.Inventorys.CreateInventory
{
    public class CreateInventoryCommandArgs
    {
        public long Quantity { get; set; }
        public float UnitPrice { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }
    }
}
