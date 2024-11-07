using RS.CommonLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RS.CommonLibrary.Constants.CommonConstants;

namespace RS.CommonLibrary.Security.Extensions
{
    public static class UserRoleExtensions
    {
        public static bool IsAdmin(this UserDto user)
        => user.Role == (int)USER_ROLE.ADMIN;

        public static bool IsShopOwner(this UserDto user)
            => user.Role == (int)USER_ROLE.SHOP_OWNER;

        public static bool IsOrderManager(this UserDto user)
            => user.Role == (int)USER_ROLE.ORDER_MANAGEMENT;

        public static string GetRoleName(this UserDto user)
            => ((USER_ROLE)user.Role).ToString();
    }
}
