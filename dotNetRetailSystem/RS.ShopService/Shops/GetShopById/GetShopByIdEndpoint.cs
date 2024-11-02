using Carter;
using Mapster;
using MediatR;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.GetShopById
{
    public record GetShopByIdResponse(Shop Shop);

    public class GetShopByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/shops/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetShopByIdQuery(id));

                    var response = result.Adapt<GetShopByIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetShopById")
                .Produces<GetShopByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Shop By Id")
                .WithDescription("Get Shop By Id");
        }
    }
}
