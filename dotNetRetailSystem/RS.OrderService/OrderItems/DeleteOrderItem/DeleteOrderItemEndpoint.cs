using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.OrderItems.DeleteOrderItem
{
    public record DeleteOrderItemResponse(bool IsSuccess);

    public class DeleteOrderItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                "/OrderItems/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteOrderItemCommand(id));

                    var response = result.Adapt<DeleteOrderItemResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("DeleteOrderItem")
                .Produces<DeleteOrderItemResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete OrderItem")
                .WithDescription("Delete OrderItem");
        }
    }
}
