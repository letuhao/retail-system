namespace RS.UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime DoB { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Country { get; set;} = default!;
        public int Role { get; set; }
        
    }
}
