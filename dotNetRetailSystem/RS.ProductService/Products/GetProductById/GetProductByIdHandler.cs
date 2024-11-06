using MongoDB;
using MongoDB.Driver;
using RS.CommonLibrary.CQRS;
using RS.ProductService.Exceptions;
using RS.ProductService.Models;

namespace RS.ProductService.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetShopByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IMongoCollection<Product> _products;

        public GetShopByIdHandler(IMongoCollection<Product> products)
        {
            _products = products;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.Id);
            var findOptions = new FindOptions<Product>
            {
                Limit = 1,
                AllowDiskUse = false
            };

            using var cursor = await _products.FindAsync(
                filter,
                findOptions,
                cancellationToken);

            var product = await cursor.FirstOrDefaultAsync(cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return new GetProductByIdResult(product);
        }
    }
}
