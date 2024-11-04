using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.GetShippingByOrderId
{
    public record GetShippingByOrderIdQuery(Guid OrderId) : IQuery<GetShippingByOrderIdResult>;

    public record GetShippingByOrderIdResult(IEnumerable<Shipping> Shippings);

    internal class GetCartItemByCartIdQueryHandler(IDocumentSession session) : IQueryHandler<GetShippingByOrderIdQuery, GetShippingByOrderIdResult>
    {
        public async Task<GetShippingByOrderIdResult> Handle(GetShippingByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var shipping = await session
                .Query<Shipping>()
                .Where(p => p.OrderId == request.OrderId)
                .ToListAsync(cancellationToken);

            return new GetShippingByOrderIdResult(shipping);
        }
    }
}
