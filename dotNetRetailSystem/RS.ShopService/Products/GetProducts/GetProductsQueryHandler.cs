using Marten;
using Marten.Pagination;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Models;

namespace RS.ShopService.Products.GetProducts
{
    public record GetProductsQuery(
        int? PageNumber = CommonConstants.PAGING_DEFAULT_FISRT_PAGE,
        int? PageSize = CommonConstants.PAGING_DEFAULT_PAGE_SIZE
        ) : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Product>()
                .ToPagedListAsync(
                    request.PageNumber ?? CommonConstants.PAGING_DEFAULT_FISRT_PAGE, 
                    request.PageSize ?? CommonConstants.PAGING_DEFAULT_PAGE_SIZE, 
                    cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
