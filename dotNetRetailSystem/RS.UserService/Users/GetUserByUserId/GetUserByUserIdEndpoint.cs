using Carter;
using Mapster;
using MediatR;
using RS.UserService.Models;

namespace RS.UserService.Users.GetUserByUserId
{
    public record GetUserByUserIdResponse(IEnumerable<User> Users);

    public class GetUserByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Users/category/{category}",
                async (string category, ISender sender) =>
                {
                    var result = await sender.Send(new GetUserByUserIdQuery(category));

                    var response = result.Adapt<GetUserByUserIdResponse>();

                    return Results.Ok(response);
                }
            )
                .WithName("GetUserByUserId")
                .Produces<GetUserByUserIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get User By Category")
                .WithDescription("Get User By Category");
        }
    }
}
