using Carter;
using Mapster;
using MediatR;

namespace RS.ShopService.Shops.DeleteShop
{
    public record DeleteShopResponse(bool IsSuccess);

    public class DeleteShopEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                "/shops/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteShopCommand(id));

                    var response = result.Adapt<DeleteShopResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("DeleteShop")
                .Produces<DeleteShopResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Shop")
                .WithDescription("Delete Shop");
        }
    }
}
