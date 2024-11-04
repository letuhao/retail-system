﻿using Carter;
using Mapster;
using MediatR;

namespace RS.OrderService.Orders.CreateOrder
{
    public record CreateOrderRequest(CreateOrderCommandArgs Args);

    public record CreateOrderResponse(Guid Id);

    public class CreateOrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/Orders",
                async (CreateOrderRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateOrderCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateOrderResponse>();

                    return Results.Created($"/Orders/{response.Id}", response);

                })
                .WithName("CreateOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order");
        }
    }
}
