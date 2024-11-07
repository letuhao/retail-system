using Marten;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.CommonLibrary.Security.UserUtils;
using RS.UserService.Exceptions;
using RS.UserService.Models;
using RS.UserService.Users.DeleteUser;

namespace RS.UserService.Users.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;

    public record GetUserByIdResult(User User);

    internal class GetUserByIdHandler(IDocumentSession session, IHttpContextAccessor httpContextAccessor) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = UserUtils.GetCurrentUser(httpContextAccessor);
            if (user.Role != (int)CommonConstants.USER_ROLE.ADMIN)
            {
                return new GetUserByIdResult(null);
            }

            var User = await session.LoadAsync<User>(request.Id, cancellationToken);

            if (User is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            return new GetUserByIdResult(User);
        }
    }
}
