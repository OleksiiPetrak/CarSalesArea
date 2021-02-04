using Microsoft.AspNetCore.Identity;

namespace CarSalesArea.Data.Models
{
    public class UserRoleEntity:IdentityRole<long>
    {
        public UserRoleEntity(): base()
        {
        }

        public UserRoleEntity(string roleName)
            :base(roleName)
        {
        }
    }
}
