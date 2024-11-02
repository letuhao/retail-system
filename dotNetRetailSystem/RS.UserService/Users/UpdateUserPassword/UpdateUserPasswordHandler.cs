using FluentValidation;
using JasperFx.Core;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.UserService.Exceptions;
using RS.UserService.Models;

namespace RS.UserService.Users.UpdateUserPassword
{
    public record UpdateUserPasswordCommand(UpdateUserPasswordCommandArgs Args) : ICommand<UpdateUserPasswordResult>;

    public record UpdateUserPasswordResult(bool IsSuccess);

    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(command => command.Args.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }

    internal class UpdateUserPasswordPasswordHandler(IDocumentSession session) : ICommandHandler<UpdateUserPasswordCommand, UpdateUserPasswordResult>
    {
        public async Task<UpdateUserPasswordResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var User = await session.LoadAsync<User>(request.Args.Id, cancellationToken);

            if (User is null)
            {
                throw new UserNotFoundException(request.Args.Id);
            }

            User.Password = request.Args.Password;

            session.Update(User);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateUserPasswordResult(true);
        }
    }
}
