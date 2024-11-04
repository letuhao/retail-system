using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.OrderItems.UpdateOrderItem
{
    public record UpdateOrderItemRequest(UpdateOrderItemCommandArgs Args);

    public record UpdateOrderItemResponse(bool IsSuccess);

    public class UpdateOrderItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/OrderItems",
                async (UpdateOrderItemRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateOrderItemCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateOrderItemResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateOrderItem")
                .Produces<UpdateOrderItemResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update OrderItem")
                .WithDescription("Update OrderItem");
        }
    }
}
