using FluentValidation;
using JasperFx.Core;
using Marten;
using Microsoft.AspNetCore.Http;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.CommonLibrary.Security.UserUtils;
using RS.UserService.Exceptions;
using RS.UserService.Models;
using RS.UserService.Users.UpdateUser;

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

    internal class UpdateUserPasswordPasswordHandler(IDocumentSession session, IHttpContextAccessor httpContextAccessor) : ICommandHandler<UpdateUserPasswordCommand, UpdateUserPasswordResult>
    {
        public async Task<UpdateUserPasswordResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = UserUtils.GetCurrentUser(httpContextAccessor);
            if (user.Role != (int)CommonConstants.USER_ROLE.ADMIN && user.Role != (int)CommonConstants.USER_ROLE.USER)
            {
                return new UpdateUserPasswordResult(false);
            }

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
