using MongoDB;
using MongoDB.Driver;
using RS.CommonLibrary.CQRS;
using RS.ProductService.Exceptions;
using RS.ProductService.Models;

namespace RS.ProductService.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryQueryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly IMongoCollection<Product> _products;

        public GetProductByCategoryQueryHandler(IMongoCollection<Product> products)
        {
            _products = products;
        }

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.AnyEq(p => p.Category, request.Category);
            var findOptions = new FindOptions<Product>
            {
                Limit = 1,
                AllowDiskUse = false
            };

            using var cursor = await _products.FindAsync(
                filter,
                findOptions,
                cancellationToken);

            var product = await cursor.ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(product);
        }
    }
}
