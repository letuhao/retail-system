using Marten;
using RS.CommonLibrary.CQRS;
using RS.UserService.Exceptions;
using RS.UserService.Models;

namespace RS.UserService.Users.GetUserById
{
    public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;

    public record GetUserByIdResult(User User);

    internal class GetUserByIdHandler(IDocumentSession session) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var User = await session.LoadAsync<User>(request.Id, cancellationToken);

            if (User is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            return new GetUserByIdResult(User);
        }
    }
}
