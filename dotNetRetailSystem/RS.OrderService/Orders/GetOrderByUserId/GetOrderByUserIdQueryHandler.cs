using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.GetOrderByCatalogy
{
    public record GetOrderByUserIdQuery(Guid UserId) : IQuery<GetOrderByUserIdResult>;

    public record GetOrderByUserIdResult(IEnumerable<Order> Orders);

    internal class GetCartItemByCartIdQueryHandler(IDocumentSession session) : IQueryHandler<GetOrderByUserIdQuery, GetOrderByUserIdResult>
    {
        public async Task<GetOrderByUserIdResult> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            var Orders = await session
                .Query<Order>()
                .Where(p => p.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return new GetOrderByUserIdResult(Orders);
        }
    }
}
