using Carter;
using Mapster;
using MediatR;

namespace RS.OrderItemService.OrderItems.CreateOrderItem
{
    public record CreateOrderItemRequest(CreateOrderItemCommandArgs Args);

    public record CreateOrderItemResponse(Guid Id);

    public class CreateOrderItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/OrderItems",
                async (CreateOrderItemRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateOrderItemCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateOrderItemResponse>();

                    return Results.Created($"/OrderItems/{response.Id}", response);

                })
                .WithName("CreateOrderItem")
                .Produces<CreateOrderItemResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create OrderItem")
                .WithDescription("Create OrderItem");
        }
    }
}
