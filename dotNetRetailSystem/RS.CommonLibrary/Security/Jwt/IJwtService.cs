using RS.CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RS.CommonLibrary.Security.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(UserDto user);
        ClaimsPrincipal ValidateToken(string token);
        string GetUserIdFromToken(string token);
        IEnumerable<string> GetUserRolesFromToken(string token);
    }
}
