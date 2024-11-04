﻿using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Orders.DeleteOrder
{
    public record DeleteOrderResponse(bool IsSuccess);

    public class DeleteOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete(
                "/Orders/{id}", 
                async (Guid id, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteOrderCommand(id));

                    var response = result.Adapt<DeleteOrderResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("DeleteOrder")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Order")
                .WithDescription("Delete Order");
        }
    }
}
