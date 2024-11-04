namespace RS.OrderService.Models
{
    public class Shipping
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EstimatedDate { get; set; }
        public DateTime DeliveredDate { get; set; }
        public string ShippingAddress { get; set; } = default!;
        public int ShippingMethod {  get; set; }
        public string RecipentName { get; set; } = default!;
        public string RecipentPhone { get; set; } = default!;
        public string CourierId { get; set; } = default!;
        public string CourierName { get; set; } = default!;
        public Guid OrderId { get; set; }
    }
}
