using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.UserService.Models;

namespace RS.UserService.Users.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;

    public record DeleteUserResult(bool IsSuccess);

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User ID is required");
        }
    }

    internal class DeleteUserHandler(IDocumentSession session) : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            session.Delete<User>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteUserResult(true);
        }
    }
}
