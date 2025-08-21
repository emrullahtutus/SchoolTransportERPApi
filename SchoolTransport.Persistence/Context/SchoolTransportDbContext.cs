using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolTransport.Domain.Entities.Activity;
using SchoolTransport.Domain.Entities.Driver;
using SchoolTransport.Domain.Entities.Expenses;
using SchoolTransport.Domain.Entities.Identity;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using System.Reflection;

namespace SchoolTransport.Persistence.Context
{
    public class SchoolTransportDbContext : IdentityDbContext<User>
    {
        public SchoolTransportDbContext(DbContextOptions<SchoolTransportDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        // Payment DbSets
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        // School Fee Structure DbSet
        public DbSet<SchoolFeeStructure> SchoolFeeStructures { get; set; }

        public DbSet<Activity> Activities  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Veritabanı performansını artırmak için TenantId kolonlarına indeks ekle.
            // Bu, çok kiracılı sorguları hızlandıracaktır.
            modelBuilder.Entity<Student>().HasIndex(s => s.TenantId);
            modelBuilder.Entity<School>().HasIndex(s => s.TenantId);
            modelBuilder.Entity<Vehicle>().HasIndex(v => v.TenantId);
            modelBuilder.Entity<Driver>().HasIndex(d => d.TenantId);
            modelBuilder.Entity<Payment>().HasIndex(p => p.TenantId);
            modelBuilder.Entity<PaymentTransaction>().HasIndex(pt => pt.TenantId);
            modelBuilder.Entity<Expense>().HasIndex(e => e.TenantId);
            modelBuilder.Entity<SchoolFeeStructure>().HasIndex(sfs => sfs.TenantId);

            // Decimal türü kolonlara kesin tip tanımlaması eklendi.
            // Bu, veri kaybı uyarılarını ortadan kaldırır.
            modelBuilder.Entity<SchoolFeeStructure>(entity =>
            {
                entity.Property(e => e.MinDistance).HasColumnType("decimal(5,2)");
                entity.Property(e => e.MaxDistance).HasColumnType("decimal(5,2)");
                entity.Property(e => e.MonthlyFee).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.MonthlyFee).HasColumnType("decimal(10,2)");
                entity.Property(e => e.TotalFee).HasColumnType("decimal(10,2)");
                entity.Property(e => e.PaidAmount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.RemainingAmount).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<PaymentTransaction>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.Fuel).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Industry).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Insurance).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Penalty).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            });
            modelBuilder.Entity<Student>().Property(e => e.MonthlyFee).HasColumnType("decimal(10,2)");


            // Diğer Konfigürasyonlar (Senin kodundan kopyalandı, TenantId indeksleri eklendi)

            // Student Configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.School)
                    .WithMany(s => s.Students)
                    .HasForeignKey(e => e.SchoolId);
                entity.HasOne(e => e.Payment)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Payment>(p => p.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Vehicle)
                    .WithMany(v => v.Students)
                    .HasForeignKey(s => s.VehicleId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // School Configuration
            modelBuilder.Entity<School>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InstallmentCount).HasDefaultValue(9);
                entity.HasMany(e => e.FeeStructures)
                    .WithOne(f => f.School)
                    .HasForeignKey(f => f.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(s => s.Vehicles)
                    .WithMany(v => v.Schools);
            });

            // SchoolFeeStructure Configuration
            modelBuilder.Entity<SchoolFeeStructure>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.HasOne(e => e.School)
                    .WithMany(s => s.FeeStructures)
                    .HasForeignKey(e => e.SchoolId);
            });

            // Vehicle Configuration
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Driver)
                    .WithOne(d => d.Vehicle)
                    .HasForeignKey<Vehicle>(e => e.DriverId);
            });

            // Driver Configuration
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // Payment Configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PaidAmount).HasDefaultValue(0);
                entity.HasOne(e => e.Student)
                    .WithOne(s => s.Payment)
                    .HasForeignKey<Payment>(e => e.StudentId);
                entity.HasMany(e => e.PaymentTransactions)
                    .WithOne(pt => pt.Payment)
                    .HasForeignKey(pt => pt.PaymentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // PaymentTransaction Configuration
            modelBuilder.Entity<PaymentTransaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Payment)
                    .WithMany(p => p.PaymentTransactions)
                    .HasForeignKey(e => e.PaymentId);
                entity.HasOne(pt => pt.Student)
                    .WithMany(s => s.PaymentTransaction)
                    .HasForeignKey(pt => pt.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Expense Configuration
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Vehicle)
                    .WithMany(v => v.Expenses)
                    .HasForeignKey(e => e.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.VehicleId)
                    .IsRequired();
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}