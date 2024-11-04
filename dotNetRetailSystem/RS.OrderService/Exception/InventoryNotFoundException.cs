using RS.CommonLibrary.Exceptions;

namespace RS.OrderService.Exceptions
{
    public class InventoryNotFoundException : NotFoundException
    {
        public InventoryNotFoundException(Guid Id) : base("Inventory", Id)
        {
        }
    }
}
