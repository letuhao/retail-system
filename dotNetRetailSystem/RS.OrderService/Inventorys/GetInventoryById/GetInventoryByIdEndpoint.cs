using Carter;
using Mapster;
using MediatR;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.GetInventoryById
{
    public record GetInventoryByIdResponse(Inventory Inventory);

    public class GetInventoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Inventorys/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new GetInventoryByIdQuery(id));

                    var response = result.Adapt<GetInventoryByIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetInventoryById")
                .Produces<GetInventoryByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Inventory By Id")
                .WithDescription("Get Inventory By Id");
        }
    }
}
