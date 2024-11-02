namespace RS.UserService.Users.UpdateUser
{
    public class UpdateUserCommandArgs
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Country { get; set; } = default!;
        public int Role { get; set; }
    }
}
