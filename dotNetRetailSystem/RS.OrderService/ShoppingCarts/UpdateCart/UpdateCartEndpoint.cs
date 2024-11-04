using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.ShoppingCarts.UpdateCart
{
    public record UpdateCartRequest(UpdateCartCommandArgs Args);

    public record UpdateCartResponse(bool IsSuccess);

    public class UpdateCartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/Carts",
                async (UpdateCartRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateCartCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateCartResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateCart")
                .Produces<UpdateCartResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Cart")
                .WithDescription("Update Cart");
        }
    }
}
