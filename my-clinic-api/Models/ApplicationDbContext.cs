﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace my_clinic_api.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public readonly DbContextOptions<ApplicationDbContext> _options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            _options = options;
            
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>(m =>
            {

                m.ToTable("Doctor");
            });
            modelBuilder.Entity<Patient>(m =>
            {
                m.ToTable("Patients");
            });
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<TimesOfWork> TimesOfWork { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<RateAndReview> RatesAndReviews { get; set; }
        public DbSet<Book> Bookings { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportReasons> ReportReasons { get; set; }
    }
}
