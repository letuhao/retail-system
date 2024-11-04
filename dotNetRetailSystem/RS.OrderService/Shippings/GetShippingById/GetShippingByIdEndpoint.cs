using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.GetShippingById
{
    public record GetShippingByIdResponse(Shipping Shipping);

    public class GetShippingByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Shippings/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetShippingByIdQuery(id));

                    var response = result.Adapt<GetShippingByIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetShippingById")
                .Produces<GetShippingByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Shipping By Id")
                .WithDescription("Get Shipping By Id");
        }
    }
}
