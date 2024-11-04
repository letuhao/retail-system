using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.CartItems.DeleteCartItem
{
    public record DeleteCartItemResponse(bool IsSuccess);

    public class DeleteCartItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                "/CartItems/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteCartItemCommand(id));

                    var response = result.Adapt<DeleteCartItemResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("DeleteCartItem")
                .Produces<DeleteCartItemResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete CartItem")
                .WithDescription("Delete CartItem");
        }
    }
}
