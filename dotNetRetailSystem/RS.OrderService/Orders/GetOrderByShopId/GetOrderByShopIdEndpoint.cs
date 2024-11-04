using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.GetOrderByCatalogy
{
    public record GetOrderByShopIdResponse(IEnumerable<Order> Orders);

    public class GetOrderByShopIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Orders/ShopId/{ShopId}",
                async (Guid ShopId, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrderByShopIdQuery(ShopId));

                    var response = result.Adapt<GetOrderByShopIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetOrderByShopId")
                .Produces<GetOrderByShopIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Order By ShopId")
                .WithDescription("Get Order By ShopId");
        }
    }
}
