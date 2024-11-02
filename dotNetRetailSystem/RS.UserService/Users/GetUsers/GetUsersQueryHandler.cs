using Marten;
using Marten.Pagination;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.UserService.Models;

namespace RS.UserService.Users.GetUsers
{
    public record GetUsersQuery(
        int? PageNumber = CommonConstants.PAGING_DEFAULT_FISRT_PAGE,
        int? PageSize = CommonConstants.PAGING_DEFAULT_PAGE_SIZE
        ) : IQuery<GetUsersResult>;

    public record GetUsersResult(IEnumerable<User> Users);

    internal class GetUsersQueryHandler(IDocumentSession session) : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
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
