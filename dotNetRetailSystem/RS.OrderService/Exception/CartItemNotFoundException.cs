using RS.CommonLibrary.Exceptions;

namespace RS.OrderService.Exceptions
{
    public class CartItemNotFoundException : NotFoundException
    {
        public CartItemNotFoundException(Guid Id) : base("CartItem", Id)
        {
        }
    }
}
