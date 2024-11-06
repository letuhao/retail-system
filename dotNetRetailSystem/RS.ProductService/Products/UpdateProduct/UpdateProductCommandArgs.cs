namespace RS.ProductService.Products.UpdateProduct
{
    public class UpdateProductCommandArgs
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public float Price { get; set; }
    }
}
