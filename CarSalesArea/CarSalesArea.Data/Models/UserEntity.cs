using System;
using Microsoft.AspNetCore.Identity;

namespace CarSalesArea.Data.Models
{
    public class UserEntity: IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
