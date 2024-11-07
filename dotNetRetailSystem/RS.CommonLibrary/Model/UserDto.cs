using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.CommonLibrary.Model
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int Role { get; set; }
    }
}
