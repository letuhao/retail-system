using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.CartItems.CreateCartItem
{
    public record CreateCartItemRequest(CreateCartItemCommandArgs Args);

    public record CreateCartItemResponse(Guid Id);

    public class CreateCartItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/CartItems",
                async (CreateCartItemRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateCartItemCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateCartItemResponse>();

                    return Results.Created($"/CartItems/{response.Id}", response);

                })
                .WithName("CreateCartItem")
                .Produces<CreateCartItemResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create CartItem")
                .WithDescription("Create CartItem");
        }
    }
}
