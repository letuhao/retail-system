using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.CartItems.UpdateCartItem
{
    public record UpdateCartItemRequest(UpdateCartItemCommandArgs Args);

    public record UpdateCartItemResponse(bool IsSuccess);

    public class UpdateCartItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/CartItems",
                async (UpdateCartItemRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateCartItemCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateCartItemResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateCartItem")
                .Produces<UpdateCartItemResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update CartItem")
                .WithDescription("Update CartItem");
        }
    }
}
