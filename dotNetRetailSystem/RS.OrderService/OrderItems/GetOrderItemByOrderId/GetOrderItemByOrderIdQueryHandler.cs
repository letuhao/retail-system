using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.GetOrderItemByCatalogy
{
    public record GetOrderItemByOrderIdQuery(Guid OrderId) : IQuery<GetOrderItemByOrderIdResult>;

    public record GetOrderItemByOrderIdResult(IEnumerable<OrderItem> OrderItems);

    internal class GetOrderItemByCartIdQueryHandler(IDocumentSession session) : IQueryHandler<GetOrderItemByOrderIdQuery, GetOrderItemByOrderIdResult>
    {
        public async Task<GetOrderItemByOrderIdResult> Handle(GetOrderItemByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var OrderItems = await session
                .Query<OrderItem>()
                .Where(p => p.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            return new GetOrderItemByOrderIdResult(OrderItems);
        }
    }
}
