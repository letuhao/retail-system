using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Shippings.DeleteShipping
{
    public record DeleteShippingResponse(bool IsSuccess);

    public class DeleteCartItemEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                "/Shippings/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteShippingCommand(id));

                    var response = result.Adapt<DeleteShippingResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("DeleteShipping")
                .Produces<DeleteShippingResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Shipping")
                .WithDescription("Delete Shipping");
        }
    }
}
