using MongoDB;
using MongoDB.Driver;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.ProductService.Models;

namespace RS.ProductService.Products.GetProducts
{
    public record GetProductsQuery(
        int? PageNumber = CommonConstants.PAGING_DEFAULT_FISRT_PAGE,
        int? PageSize = CommonConstants.PAGING_DEFAULT_PAGE_SIZE
        ) : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler: IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly IMongoCollection<Product> _products;

        public GetProductsQueryHandler(IMongoCollection<Product> products)
        {
            _products = products;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Empty;
            var findOptions = new FindOptions<Product>
            {
                Skip = request.PageNumber ?? CommonConstants.PAGING_DEFAULT_FISRT_PAGE,
                Limit = request.PageSize ?? CommonConstants.PAGING_DEFAULT_PAGE_SIZE,
                AllowDiskUse = false
            };

            using var cursor = await _products.FindAsync(
                filter,
                findOptions,
                cancellationToken);

            var product = await cursor.ToListAsync(cancellationToken);

            return new GetProductsResult(product);
        }
    }
}
