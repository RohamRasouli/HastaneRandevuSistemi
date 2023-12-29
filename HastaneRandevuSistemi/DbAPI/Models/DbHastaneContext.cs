using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbAPI.Models
{
    public partial class DbHastaneContext : DbContext
    {
        public DbHastaneContext()
        {
        }

        public DbHastaneContext(DbContextOptions<DbHastaneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<DoctorWorkTime> DoctorWorkTimes { get; set; } = null!;
        public virtual DbSet<DoctorsMainScienceBranch> DoctorsMainScienceBranches { get; set; } = null!;
        public virtual DbSet<MainScienceBranch> MainScienceBranches { get; set; } = null!;
        public virtual DbSet<Policlinic> Policlinics { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-28GDF39\\SQLEXPRESS;Database=DbHastane;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.CreatetDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.MainScienceBranchId).HasColumnName("MainScienceBranchID");

                entity.Property(e => e.PoliclinicId).HasColumnName("PoliclinicID");

                entity.Property(e => e.StatuId).HasColumnName("StatuID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<DoctorWorkTime>(entity =>
            {
                entity.HasKey(e => e.DoctorWorkTimesId)
                    .HasName("PK__DoctorWo__EBEB418A299A9D9B");

                entity.ToTable("DoctorWorkTime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.MainBranchId).HasColumnName("MainBranchID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DoctorsMainScienceBranch>(entity =>
            {
                entity.HasKey(e => e.DoctorScienceId)
                    .HasName("PK__DoctorsM__21866EFE1CFA0F09");

                entity.ToTable("DoctorsMainScienceBranch");

                entity.Property(e => e.DoctorScienceId).HasColumnName("DoctorScienceID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.MainScienceBranchId).HasColumnName("MainScienceBranchID");
            });

            modelBuilder.Entity<MainScienceBranch>(entity =>
            {
                entity.HasKey(e => e.MainScienceBranchıd)
                    .HasName("PK__MainScie__1F92762D74FCB6ED");

                entity.ToTable("MainScienceBranch");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ScienceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Policlinic>(entity =>
            {
                entity.ToTable("Policlinic");

                entity.Property(e => e.PoliclinicId).HasColumnName("PoliclinicID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PoliclinicName).HasMaxLength(100);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StatuId)
                    .HasName("PK__Status__C7DB7B799DACEE17");

                entity.ToTable("Status");

                entity.Property(e => e.StatuId).HasColumnName("StatuID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.StatuName).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PhotoUrl).HasMaxLength(500);

                entity.Property(e => e.UserEmail).HasMaxLength(250);

                entity.Property(e => e.UserFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserSecondName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
