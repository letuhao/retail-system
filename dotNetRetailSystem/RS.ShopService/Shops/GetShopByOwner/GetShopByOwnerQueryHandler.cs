using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.GetShopByCatalogy
{
    public record GetShopByOwnerQuery(string Owner) : IQuery<GetShopByOwnerResult>;

    public record GetShopByOwnerResult(IEnumerable<Shop> Shops);

    internal class GetShopByOwnerQueryHandler(IDocumentSession session) : IQueryHandler<GetShopByOwnerQuery, GetShopByOwnerResult>
    {
        public async Task<GetShopByOwnerResult> Handle(GetShopByOwnerQuery request, CancellationToken cancellationToken)
        {
            var Shops = await session
                .Query<Shop>()
                .Where(p => p.Owner.Contains(request.Owner))
                .ToListAsync(cancellationToken);

            return new GetShopByOwnerResult(Shops);
        }
    }
}
