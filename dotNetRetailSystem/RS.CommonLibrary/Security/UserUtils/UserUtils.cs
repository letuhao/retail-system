using Microsoft.AspNetCore.Http;
using RS.CommonLibrary.Model;
using System.Security.Claims;

namespace RS.CommonLibrary.Security.UserUtils
{
    public class UserUtils
    {
        public static UserDto GetCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var claims = httpContextAccessor.HttpContext?.User;

            return new UserDto
            {
                Id = Guid.Parse(claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty),
                Email = claims?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
                Name = claims?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                Role = int.Parse(claims?.FindFirst("Role")?.Value ?? "1") // Default to Customer
            };
        }
    }
}
