using Carter;
using Mapster;
using MediatR;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.GetShopByCatalogy
{
    public record GetShopByOwnerResponse(IEnumerable<Shop> Shops);

    public class GetShopByOwnerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/shops/owner/{owner}",
                async (string owner, ISender sender) =>
                {
                    var result = await sender.Send(new GetShopByOwnerQuery(owner));

                    var response = result.Adapt<GetShopByOwnerResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetShopByOwner")
                .Produces<GetShopByOwnerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Shop By Owner")
                .WithDescription("Get Shop By Owner");
        }
    }
}
