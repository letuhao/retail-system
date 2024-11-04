using RS.CommonLibrary.Exceptions;

namespace RS.OrderService.Exceptions
{
    public class ShippingNotFoundException : NotFoundException
    {
        public ShippingNotFoundException(Guid Id) : base("Shipping", Id)
        {
        }
    }
}
