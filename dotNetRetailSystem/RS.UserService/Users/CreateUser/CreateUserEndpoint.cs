using Carter;
using Mapster;
using MediatR;

namespace RS.UserService.Users.CreateUser
{
    public record CreateUserRequest(CreateUserCommandArgs Args);

    public record CreateUserResponse(Guid Id);

    public class CreateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/Users",
                async (CreateUserRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateUserCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateUserResponse>();

                    return Results.Created($"/Users/{response.Id}", response);

                })
                .WithName("CreateUser")
                .Produces<CreateUserResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create User")
                .WithDescription("Create User");
        }
    }
}
