namespace RS.ShopService.Shops.CreateShop
{
    public class CreateShopCommandArgs
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public string Owner { get; set; } = default!;
    }
}
