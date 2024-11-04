using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.GetInventoryByShopId
{
    public record GetInventoryByShopIdResponse(IEnumerable<Inventory> Inventorys);

    public class GetInventoryByShopIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Inventorys/ShopId/{ShopId}",
                async (Guid ShopId, ISender sender) =>
                {
                    var result = await sender.Send(new GetInventoryByShopIdQuery(ShopId));

                    var response = result.Adapt<GetInventoryByShopIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetInventoryByShopId")
                .Produces<GetInventoryByShopIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Inventory By ShopId")
                .WithDescription("Get Inventory By ShopId");
        }
    }
}
