using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.ShoppingCarts.CreateCart
{
    public record CreateCartRequest(CreateCartCommandArgs Args);

    public record CreateCartResponse(Guid Id);

    public class CreateCartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/Carts",
                async (CreateCartRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateCartCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateCartResponse>();

                    return Results.Created($"/Carts/{response.Id}", response);

                })
                .WithName("CreateCart")
                .Produces<CreateCartResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Cart")
                .WithDescription("Create Cart");
        }
    }
}
