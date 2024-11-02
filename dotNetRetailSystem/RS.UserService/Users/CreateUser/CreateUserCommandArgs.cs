namespace RS.UserService.Users.CreateUser
{
    public class CreateUserCommandArgs
    {
        public string UserId { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime DoB { get; set; } = default!;
        public int Gender { get; set; }
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
