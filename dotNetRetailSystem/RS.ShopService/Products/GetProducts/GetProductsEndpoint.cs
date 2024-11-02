﻿using Carter;
using Mapster;
using MediatR;
using RS.CommonLibrary.Constants;
using RS.ShopService.Models;

namespace RS.ShopService.Products.GetProducts
{
    public record GetProductsRequest(
        int? PageNumber = CommonConstants.PAGING_DEFAULT_FISRT_PAGE, 
        int? PageSize = CommonConstants.PAGING_DEFAULT_PAGE_SIZE);

    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/products", 
                async ([AsParameters] GetProductsRequest request, ISender sender) =>
                {
                    var query = request.Adapt<GetProductsQuery>();

                    var result = await sender.Send(query);

                    var response = result.Adapt<GetProductsResponse>();

                    return Results.Ok(response);
                }
            )
                 .WithName("GetProducts")
                 .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Get Products")
                 .WithDescription("Get Products");
        }
    }
}