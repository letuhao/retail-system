using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.GetCartItemById
{
    public record GetCartItemByIdResponse(CartItem CartItem);

    public class GetCartItemByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/CartItems/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetCartItemByIdQuery(id));

                    var response = result.Adapt<GetCartItemByIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetCartItemById")
                .Produces<GetCartItemByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get CartItem By Id")
                .WithDescription("Get CartItem By Id");
        }
    }
}
