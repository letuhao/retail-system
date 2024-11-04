using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Shippings.CreateShipping
{
    public record CreateShippingRequest(CreateShippingCommandArgs Args);

    public record CreateShippingResponse(Guid Id);

    public class CreateShippingEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/Shippings",
                async (CreateShippingRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateShippingCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateShippingResponse>();

                    return Results.Created($"/Shippings/{response.Id}", response);

                })
                .WithName("CreateShipping")
                .Produces<CreateShippingResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Shipping")
                .WithDescription("Create Shipping");
        }
    }
}
