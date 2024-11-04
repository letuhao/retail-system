using RS.CommonLibrary.Exceptions;

namespace RS.OrderService.Exceptions
{
    public class OrderItemNotFoundException : NotFoundException
    {
        public OrderItemNotFoundException(Guid Id) : base("OrderItem", Id)
        {
        }
    }
}
