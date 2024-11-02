namespace RS.UserService.Users.UpdateUserPassword
{
    public class UpdateUserPasswordCommandArgs
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = default!;
    }
}
