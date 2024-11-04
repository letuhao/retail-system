using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.GetOrderByCatalogy
{
    public record GetOrderByUserIdResponse(IEnumerable<Order> Orders);

    public class GetOrderByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Orders/UserId/{UserId}",
                async (Guid UserId, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrderByUserIdQuery(UserId));

                    var response = result.Adapt<GetOrderByUserIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetOrderByUserId")
                .Produces<GetOrderByUserIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Order By UserId")
                .WithDescription("Get Order By UserId");
        }
    }
}
