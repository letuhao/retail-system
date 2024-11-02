using Carter;
using Mapster;
using MediatR;

namespace RS.ShopService.Shops.UpdateShop
{
    public record UpdateShopRequest(UpdateShopCommandArgs Args);

    public record UpdateShopResponse(bool IsSuccess);

    public class UpdateShopEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/shops",
                async (UpdateShopRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateShopCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateShopResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateShop")
                .Produces<UpdateShopResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Shop")
                .WithDescription("Update Shop");
        }
    }
}
