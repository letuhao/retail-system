using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Exceptions;
using RS.ShopService.Models;

namespace RS.ShopService.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetShopByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
