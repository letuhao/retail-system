using Marten;
using Marten.Pagination;
using Microsoft.AspNetCore.Http;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.CommonLibrary.Security.UserUtils;
using RS.UserService.Models;
using RS.UserService.Users.GetUserById;

namespace RS.UserService.Users.GetUsers
{
    public record GetUsersQuery(
        int? PageNumber = CommonConstants.PAGING_DEFAULT_FISRT_PAGE,
        int? PageSize = CommonConstants.PAGING_DEFAULT_PAGE_SIZE
        ) : IQuery<GetUsersResult>;

    public record GetUsersResult(IEnumerable<User> Users);

    internal class GetUsersQueryHandler(IDocumentSession session, IHttpContextAccessor httpContextAccessor) : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var user = UserUtils.GetCurrentUser(httpContextAccessor);
            if (user.Role != (int)CommonConstants.USER_ROLE.ADMIN)
            {
                return new GetUsersResult(null);
            }

            var Users = await session
                .Query<User>()
                .ToPagedListAsync(
                    request.PageNumber ?? CommonConstants.PAGING_DEFAULT_FISRT_PAGE, 
                    request.PageSize ?? CommonConstants.PAGING_DEFAULT_PAGE_SIZE, 
                    cancellationToken);

            return new GetUsersResult(Users);
        }
    }
}
