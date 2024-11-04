using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Orders.UpdateOrder
{
    public record UpdateOrderRequest(UpdateOrderCommandArgs Args);

    public record UpdateOrderResponse(bool IsSuccess);

    public class UpdateOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/Orders",
                async (UpdateOrderRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateOrderCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateOrderResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Order")
                .WithDescription("Update Order");
        }
    }
}
