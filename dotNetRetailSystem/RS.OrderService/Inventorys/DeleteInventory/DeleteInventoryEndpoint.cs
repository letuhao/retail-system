using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Inventorys.DeleteInventory
{
    public record DeleteInventoryResponse(bool IsSuccess);

    public class DeleteInventoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                "/Inventorys/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteInventoryCommand(id));

                    var response = result.Adapt<DeleteInventoryResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("DeleteInventory")
                .Produces<DeleteInventoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Inventory")
                .WithDescription("Delete Inventory");
        }
    }
}
