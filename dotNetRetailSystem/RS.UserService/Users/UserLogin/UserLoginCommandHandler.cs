using RS.CommonLibrary.Security.Jwt;
using MediatR;
using RS.UserService.Users.GetUserByUserId;
using FluentValidation;
using Marten;
using RS.CommonLibrary.Constants;
using RS.CommonLibrary.CQRS;
using RS.UserService.Users.UserLogin;

namespace RS.UserService.Users.UserLogin
{
    public record UserLoginCommand(UserLoginCommandArgs Args) : ICommand<UserLoginResponse>;

    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(x => x.Args.LoginId).NotEmpty().WithMessage("UserId Or Email is required");
            RuleFor(x => x.Args.Password).NotEmpty().WithMessage("Password is required");
        }
    }

    internal class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, UserLoginResponse>
    {
        private readonly JwtService _jwtService;
        private readonly IMediator _mediator;

        public UserLoginCommandHandler(JwtService jwtService, IMediator mediator)
        {
            _jwtService = jwtService;
            _mediator = mediator;
        }

        public async Task<UserLoginResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByUserIdQuery(request.Args.LoginId), cancellationToken);

            if (user.Users == null || user.Users.Password == request.Args.Password)
                throw new UnauthorizedAccessException("Invalid credentials");

            return new UserLoginResponse
            {
                Token = _jwtService.GenerateToken(user.Users.ToDto()),
                User = user.Users.ToDto()
            };
        }
    }
}
