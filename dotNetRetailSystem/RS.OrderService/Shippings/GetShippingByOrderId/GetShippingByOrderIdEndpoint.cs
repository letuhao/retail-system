using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.GetShippingByOrderId
{
    public record GetShippingByOrderIdResponse(IEnumerable<Shipping> Shippings);

    public class GetCartItemByOrderIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Shippings/OrderId/{OrderId}",
                async (Guid OrderId, ISender sender) =>
                {
                    var result = await sender.Send(new GetShippingByOrderIdQuery(OrderId));

                    var response = result.Adapt<GetShippingByOrderIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetShippingByOrderId")
                .Produces<GetShippingByOrderIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Shipping By OrderId")
                .WithDescription("Get Shipping By OrderId");
        }
    }
}
