using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using my_clinic_api.Models.M2M;

namespace my_clinic_api.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>(m =>
            {

                m.ToTable("Doctor");
            });

            modelBuilder.Entity<Doctor_Specialist>().HasKey(ds => new { ds.SpecialistId, ds.DoctorId});

            modelBuilder.Entity<Doctor_Specialist>().HasOne(d => d.doctor).WithMany(s => s.DoctorSpecialist)
                .HasForeignKey(i => i.DoctorId);

            modelBuilder.Entity<Doctor_Specialist>().HasOne(d => d.Specialist).WithMany(s => s.DoctorSpecialist)
              .HasForeignKey(i => i.SpecialistId);

            modelBuilder.Entity<Doctor_Hospital>().HasKey(h => new { h.HospitalId, h.DoctorId });

            modelBuilder.Entity<Doctor_Hospital>().HasOne(d => d.doctor).WithMany(h => h.DoctorHospital)
                .HasForeignKey(i => i.DoctorId);

            modelBuilder.Entity<Doctor_Hospital>().HasOne(d => d.Hospital).WithMany(s => s.DoctorHospital)
              .HasForeignKey(i => i.HospitalId);

            modelBuilder.Entity<Doctor_Insurance>().HasKey(h => new { h.InsuranceId, h.DoctorId });

            modelBuilder.Entity<Doctor_Insurance>().HasOne(d => d.doctor).WithMany(h => h.DoctorInsurance)
                .HasForeignKey(i => i.DoctorId);

            modelBuilder.Entity<Doctor_Insurance>().HasOne(d => d.Insurance).WithMany(s => s.DoctorInsurance)
              .HasForeignKey(i => i.InsuranceId);
        }

        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<TimesOfWork> TimesOfWork { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<RateAndReview> RatesAndReviews { get; set; }
        public DbSet<Doctor_Specialist> Doctor_Specialist { get; set; }
        public DbSet<Doctor_Hospital> Doctor_Hospital { get; set; }
        public DbSet<Doctor_Insurance> Doctor_Insurance { get; set; }
    }
}
