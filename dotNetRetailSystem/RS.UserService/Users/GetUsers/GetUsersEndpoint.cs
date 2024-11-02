using Carter;
using Mapster;
using MediatR;
using RS.CommonLibrary.Constants;
using RS.UserService.Models;

namespace RS.UserService.Users.GetUsers
{
    public record GetUsersRequest(
        int? PageNumber = CommonConstants.PAGING_DEFAULT_FISRT_PAGE, 
        int? PageSize = CommonConstants.PAGING_DEFAULT_PAGE_SIZE);

    public record GetUsersResponse(IEnumerable<User> Users);

    public class GetUsersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/Users", 
                async ([AsParameters] GetUsersRequest request, ISender sender) =>
                {
                    var query = request.Adapt<GetUsersQuery>();

                    var result = await sender.Send(query);

                    var response = result.Adapt<GetUsersResponse>();

                    return Results.Ok(response);
                }
            )
                 .WithName("GetUsers")
                 .Produces<GetUsersResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Get Users")
                 .WithDescription("Get Users");
        }
    }
}
