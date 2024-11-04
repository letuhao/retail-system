using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.GetOrderItemById
{
    public record GetOrderItemByIdResponse(OrderItem OrderItem);

    public class GetOrderItemByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/OrderItems/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetOrderItemByIdQuery(id));

                    var response = result.Adapt<GetOrderItemByIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetOrderItemById")
                .Produces<GetOrderItemByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get OrderItem By Id")
                .WithDescription("Get OrderItem By Id");
        }
    }
}
