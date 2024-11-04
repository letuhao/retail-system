using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.GetInventoryByShopId
{
    public record GetInventoryByShopIdQuery(Guid ShopId) : IQuery<GetInventoryByShopIdResult>;

    public record GetInventoryByShopIdResult(IEnumerable<Inventory> Inventorys);

    internal class GetInventoryByCartIdQueryHandler(IDocumentSession session) : IQueryHandler<GetInventoryByShopIdQuery, GetInventoryByShopIdResult>
    {
        public async Task<GetInventoryByShopIdResult> Handle(GetInventoryByShopIdQuery request, CancellationToken cancellationToken)
        {
            var Inventorys = await session
                .Query<Inventory>()
                .Where(p => p.ShopId == request.ShopId)
                .ToListAsync(cancellationToken);

            return new GetInventoryByShopIdResult(Inventorys);
        }
    }
}
