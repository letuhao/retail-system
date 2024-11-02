using Carter;
using Mapster;
using MediatR;

namespace RS.UserService.Users.UpdateUserPassword
{
    public record UpdateUserPasswordRequest(UpdateUserPasswordCommandArgs Args);

    public record UpdateUserPasswordResponse(bool IsSuccess);

    public class UpdateUserPasswordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "/Users",
                async (UpdateUserPasswordRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateUserPasswordCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateUserPasswordResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("UpdateUserPassword")
                .Produces<UpdateUserPasswordResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update User")
                .WithDescription("Update User");
        }
    }
}
