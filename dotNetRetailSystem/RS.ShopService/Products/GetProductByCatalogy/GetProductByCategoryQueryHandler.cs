using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Models;

namespace RS.ShopService.Products.GetProductByCatalogy
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetShopByOwnerQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
