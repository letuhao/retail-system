namespace RS.ShopService.Shops.UpdateShop
{
    public class UpdateShopCommandArgs
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
    }
}
