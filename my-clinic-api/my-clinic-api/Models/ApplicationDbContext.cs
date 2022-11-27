using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace my_clinic_api.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<TimesOfWork> TimesOfWork { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<RateAndReview> RatesAndReviews { get; set; }
    }
}
