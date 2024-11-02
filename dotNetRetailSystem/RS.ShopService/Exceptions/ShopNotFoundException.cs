using RS.CommonLibrary.Exceptions;

namespace RS.ShopService.Exceptions
{
    public class ShopNotFoundException : NotFoundException
    {
        public ShopNotFoundException(Guid Id) : base("Shop", Id)
        {
        }
    }
}
