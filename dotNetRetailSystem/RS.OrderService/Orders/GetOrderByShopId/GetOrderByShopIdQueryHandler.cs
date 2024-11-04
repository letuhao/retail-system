using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.GetOrderByCatalogy
{
    public record GetOrderByShopIdQuery(Guid ShopId) : IQuery<GetOrderByShopIdResult>;

    public record GetOrderByShopIdResult(IEnumerable<Order> Orders);

    internal class GetOrderByCartIdQueryHandler(IDocumentSession session) : IQueryHandler<GetOrderByShopIdQuery, GetOrderByShopIdResult>
    {
        public async Task<GetOrderByShopIdResult> Handle(GetOrderByShopIdQuery request, CancellationToken cancellationToken)
        {
            var Orders = await session
                .Query<Order>()
                .Where(p => p.ShopId == request.ShopId)
                .ToListAsync(cancellationToken);

            return new GetOrderByShopIdResult(Orders);
        }
    }
}
