using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.GetOrderItemByCatalogy
{
    public record GetOrderItemByOrderIdResponse(IEnumerable<OrderItem> OrderItems);

    public class GetOrderItemByOrderIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/OrderItems/OrderId/{OrderId}",
                async (Guid OrderId, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrderItemByOrderIdQuery(OrderId));

                    var response = result.Adapt<GetOrderItemByOrderIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetOrderItemByOrderId")
                .Produces<GetOrderItemByOrderIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get OrderItem By OrderId")
                .WithDescription("Get OrderItem By OrderId");
        }
    }
}
