using RS.CommonLibrary.Exceptions;

namespace RS.UserService.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid Id) : base("User", Id)
        {
        }
    }
}
