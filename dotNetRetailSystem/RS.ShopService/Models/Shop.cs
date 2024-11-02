namespace RS.ShopService.Models
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public List<Product> Products { get; set; } = new();
        public string Owner { get; set; } = default!;
    }
}
