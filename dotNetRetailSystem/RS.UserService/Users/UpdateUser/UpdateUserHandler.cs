using FluentValidation;
using JasperFx.Core;
using Marten;
using Microsoft.AspNetCore.Http;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.CommonLibrary.Security.UserUtils;
using RS.UserService.Exceptions;
using RS.UserService.Models;
using RS.UserService.Users.GetUsers;

namespace RS.UserService.Users.UpdateUser
{
    public record UpdateUserCommand(UpdateUserCommandArgs Args) : ICommand<UpdateUserResult>;

    public record UpdateUserResult(bool IsSuccess);

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(command => command.Args.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Args.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(command => command.Args.DoB)
                .NotEmpty().WithMessage("DoB is required");

            RuleFor(command => command.Args.Gender)
                .NotEmpty().WithMessage("Gender is required");

            RuleFor(command => command.Args.Phone)
                .NotEmpty().WithMessage("Phone is required");
            
            RuleFor(command => command.Args.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(command => command.Args.Role)
                .LessThan(0).GreaterThan(4).WithMessage("Role must between 0 - 4");
        }
    }

    internal class UpdateUserHandler(IDocumentSession session, IHttpContextAccessor httpContextAccessor) : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        public async Task<UpdateUserResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = UserUtils.GetCurrentUser(httpContextAccessor);
            if (user.Role != (int)CommonConstants.USER_ROLE.ADMIN && user.Role != (int)CommonConstants.USER_ROLE.USER)
            {
                return new UpdateUserResult(false);
            }

            var User = await session.LoadAsync<User>(request.Args.Id, cancellationToken);

            if (User is null)
            {
                throw new UserNotFoundException(request.Args.Id);
            }

            User.Password = request.Args.Password;
            User.Name = request.Args.Name;
            User.DoB = request.Args.DoB;
            User.Gender = request.Args.Gender;
            User.Address = request.Args.Address;
            User.Phone = request.Args.Phone;
            User.Country = request.Args.Country;
            User.Role = request.Args.Role;

            session.Update(User);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateUserResult(true);
        }
    }
}
