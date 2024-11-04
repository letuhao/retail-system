using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Shippings.UpdateShipping
{
    public record UpdateShippingRequest(UpdateShippingCommandArgs Args);

    public record UpdateShippingResponse(bool IsSuccess);

    public class UpdateShippingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/Shippings",
                async (UpdateShippingRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateShippingCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateShippingResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateShipping")
                .Produces<UpdateShippingResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Shipping")
                .WithDescription("Update Shipping");
        }
    }
}
