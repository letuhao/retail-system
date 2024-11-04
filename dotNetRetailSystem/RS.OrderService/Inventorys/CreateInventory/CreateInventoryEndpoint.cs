using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Inventorys.CreateInventory
{
    public record CreateInventoryRequest(CreateInventoryCommandArgs Args);

    public record CreateInventoryResponse(Guid Id);

    public class CreateInventoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/Inventorys",
                async (CreateInventoryRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateInventoryCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateInventoryResponse>();

                    return Results.Created($"/Inventorys/{response.Id}", response);

                })
                .WithName("CreateInventory")
                .Produces<CreateInventoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Inventory")
                .WithDescription("Create Inventory");
        }
    }
}
