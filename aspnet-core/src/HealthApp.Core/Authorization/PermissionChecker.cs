using Abp.Authorization;
using HealthApp.Authorization.Roles;
using HealthApp.Authorization.Users;

namespace HealthApp.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
