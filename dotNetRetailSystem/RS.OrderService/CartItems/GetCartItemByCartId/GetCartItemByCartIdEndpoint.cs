using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.GetCartItemByCartId
{
    public record GetCartItemByCartIdResponse(IEnumerable<CartItem> CartItems);

    public class GetCartItemByCartIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/CartItems/cartId/{cartId}",
                async (Guid cartId, ISender sender) =>
                {
                    var result = await sender.Send(new GetCartItemByCartIdQuery(cartId));

                    var response = result.Adapt<GetCartItemByCartIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetCartItemByCartId")
                .Produces<GetCartItemByCartIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get CartItem By CartId")
                .WithDescription("Get CartItem By CartId");
        }
    }
}
