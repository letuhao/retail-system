using RS.CommonLibrary.Exceptions;

namespace RS.OrderService.Exceptions
{
    public class ShoppingCartNotFoundException : NotFoundException
    {
        public ShoppingCartNotFoundException(Guid Id) : base("ShoppingCart", Id)
        {
        }
    }
}
