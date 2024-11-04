using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Inventorys.UpdateInventory
{
    public record UpdateInventoryRequest(UpdateInventoryCommandArgs Args);

    public record UpdateInventoryResponse(bool IsSuccess);

    public class UpdateInventoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/Inventorys",
                async (UpdateInventoryRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateInventoryCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateInventoryResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateInventory")
                .Produces<UpdateInventoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Inventory")
                .WithDescription("Update Inventory");
        }
    }
}
