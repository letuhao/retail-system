namespace RS.ShopService.Products.CreateProduct
{
    public class CreateProductCommandArgs
    {
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public Guid ShopId { get; set; }
    }
}
