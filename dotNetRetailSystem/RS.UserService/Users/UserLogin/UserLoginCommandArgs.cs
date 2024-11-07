using MediatR;
using RS.CommonLibrary.Model;

namespace RS.UserService.Users.UserLogin
{
    public class UserLoginCommandArgs : IRequest<UserLoginResponse>
    {
        public string LoginId { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class UserLoginResponse
    {
        public string Token { get; set; } = default!;
        public UserDto User { get; set; } = default!;
    }
}
