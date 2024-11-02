using Carter;
using Mapster;
using MediatR;

namespace RS.ShopService.Shops.CreateShop
{
    public record CreateShopRequest(CreateShopCommandArgs Args);

    public record CreateShopResponse(Guid Id);

    public class CreateShopEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/shops",
                async (CreateShopRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateShopCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateShopResponse>();

                    return Results.Created($"/Shops/{response.Id}", response);

                })
                .WithName("CreateShop")
                .Produces<CreateShopResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Shop")
                .WithDescription("Create Shop");
        }
    }
}
