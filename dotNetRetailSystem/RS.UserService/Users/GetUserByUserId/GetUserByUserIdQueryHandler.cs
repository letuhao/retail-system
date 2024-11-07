using Marten;
using RS.CommonLibrary.CQRS;
using RS.UserService.Exceptions;
using RS.UserService.Models;

namespace RS.UserService.Users.GetUserByUserId
{
    public record GetUserByUserIdQuery(string loginId) : IQuery<GetUserByUserIdResult>;

    public record GetUserByUserIdResult(User? Users);

    internal class GetShopByOwnerQueryHandler(IDocumentSession session) : IQueryHandler<GetUserByUserIdQuery, GetUserByUserIdResult>
    {
        public async Task<GetUserByUserIdResult> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            var Users = await session
                .Query<User>()
                .Where(p => p.UserId.Contains(request.loginId) || p.Email.Contains(request.loginId))
                .FirstOrDefaultAsync(cancellationToken);

            return new GetUserByUserIdResult(Users);
        }
    }
}
