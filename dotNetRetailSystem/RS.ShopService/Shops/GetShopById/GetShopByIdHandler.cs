using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Exceptions;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.GetShopById
{
    public record GetShopByIdQuery(Guid Id) : IQuery<GetShopByIdResult>;

    public record GetShopByIdResult(Shop Shop);

    internal class GetShopByIdHandler(IDocumentSession session) : IQueryHandler<GetShopByIdQuery, GetShopByIdResult>
    {
        public async Task<GetShopByIdResult> Handle(GetShopByIdQuery request, CancellationToken cancellationToken)
        {
            var shop = await session.LoadAsync<Shop>(request.Id, cancellationToken);

            if (shop is null)
            {
                throw new ShopNotFoundException(request.Id);
            }

            return new GetShopByIdResult(shop);
        }
    }
}
