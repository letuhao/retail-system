using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.UserService.Users.CreateUser;
using RS.UserService.Models;

using RS.CommonLibrary.Constants;
using System.Security.Claims;
using RS.CommonLibrary.Model;
using RS.CommonLibrary.Security.Extensions;

namespace RS.UserService.Users.CreateUser
{
    public record CreateUserCommand(CreateUserCommandArgs Args) : ICommand<CreateUserResult>;

    public record CreateUserResult(Guid Id);

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Args.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.Args.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Args.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Args.DoB).NotEmpty().WithMessage("DoB is required");
            RuleFor(x => x.Args.Email).NotEmpty().WithMessage("Email must be greater than 0");
            RuleFor(x => x.Args.Phone).NotEmpty().WithMessage("Phone must be greater than 0");
        }
    }

    internal class CreateUserCommandHandler(IDocumentSession session, IHttpContextAccessor httpContextAccessor) : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //create User entity from command object
            //save to database
            //return CreateUserResult result

            var User = new User
            {
                UserId = request.Args.UserId,
                Password = request.Args.Password,
                Name = request.Args.Name,
                DoB = request.Args.DoB,
                Address = request.Args.Address,
                Phone = request.Args.Phone,
                Email = request.Args.Email,
                Country = request.Args.Country,
                Role = (int)CommonConstants.USER_ROLE.GUEST
            };

            //save to database
            session.Store(User);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateUserResult(User.Id);
        }

    }
}
