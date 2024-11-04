using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.ShoppingCarts.GetCartById
{
    public record GetCartByIdResponse(ShoppingCart Cart);

    public class GetCartByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Carts/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetCartByIdQuery(id));

                    var response = result.Adapt<GetCartByIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetCartById")
                .Produces<GetCartByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Cart By Id")
                .WithDescription("Get Cart By Id");
        }
    }
}
