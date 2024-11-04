using RS.CommonLibrary.Exceptions;

namespace RS.OrderService.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid Id) : base("Order", Id)
        {
        }
    }
}
