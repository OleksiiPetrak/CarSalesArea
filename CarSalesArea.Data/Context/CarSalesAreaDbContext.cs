using CarSalesArea.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarSalesArea.Data.Context
{
    class CarSalesAreaDbContext : IdentityDbContext<ApplicationUser>
    {
        public CarSalesAreaDbContext(
            DbContextOptions<CarSalesAreaDbContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
