using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsTrackerApp.Models;

namespace SportsTrackerApp.Context
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<UserModel>()
            //    .Property(u => u.Initials)
            //    .HasMaxLength(5);

            //builder.HasDefaultSchema("Identity");
        }
    }
}
