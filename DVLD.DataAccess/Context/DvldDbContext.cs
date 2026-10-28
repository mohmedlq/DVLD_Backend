using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DVLD.Domain.Entities;
namespace DVLD.DataAccess.Context;

public partial class DvldDbContext : DbContext
{
    public DvldDbContext(DbContextOptions<DvldDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Deleted_License> Deleted_Licenses { get; set; }

    public virtual DbSet<DetainedLicense> DetainedLicenses { get; set; }

    public virtual DbSet<DetainedLicenses_View> DetainedLicenses_Views { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Drivers_View> Drivers_Views { get; set; }

    public virtual DbSet<InternationalLicense> InternationalLicenses { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<LicenseClass> LicenseClasses { get; set; }

    public virtual DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }

    public virtual DbSet<LocalDrivingLicenseApplications_View> LocalDrivingLicenseApplications_Views { get; set; }

    public virtual DbSet<LocalDrivingLicenseFullApplications_View> LocalDrivingLicenseFullApplications_Views { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Massage> Massages { get; set; }

    public virtual DbSet<OldUserNamesLog> OldUserNamesLogs { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestAppointment> TestAppointments { get; set; }

    public virtual DbSet<TestAppointments_View> TestAppointments_Views { get; set; }

    public virtual DbSet<TestType> TestTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AI");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.ApplicationStatus)
                .HasDefaultValue((byte)1)
                .HasComment("1-New 2-Cancelled 3-Completed");
            entity.Property(e => e.LastStatusDate).HasColumnType("datetime");
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");

            entity.HasOne(d => d.ApplicantPerson).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicantPersonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_People");

            entity.HasOne(d => d.ApplicationType).WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicationTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_ApplicationTypes");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Applications)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Users");
        });

        modelBuilder.Entity<ApplicationType>(entity =>
        {
            entity.Property(e => e.ApplicationFees).HasColumnType("smallmoney");
            entity.Property(e => e.ApplicationTypeTitle).HasMaxLength(150);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryID).HasName("PK__Countrie__10D160BFDBD6933F");

            entity.Property(e => e.CountryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Deleted_License>(entity =>
        {
            entity.HasKey(e => e.DeletedID).HasName("PK__Deleted___ABB7422597539328");

            entity.Property(e => e.DeletionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");
        });

        modelBuilder.Entity<DetainedLicense>(entity =>
        {
            entity.HasKey(e => e.DetainID);

            entity.Property(e => e.DetainDate).HasColumnType("smalldatetime");
            entity.Property(e => e.FineFees).HasColumnType("smallmoney");
            entity.Property(e => e.ReleaseDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.DetainedLicenseCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetainedLicenses_Users");

            entity.HasOne(d => d.License).WithMany(p => p.DetainedLicenses)
                .HasForeignKey(d => d.LicenseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetainedLicenses_Licenses");

            entity.HasOne(d => d.ReleaseApplication).WithMany(p => p.DetainedLicenses)
                .HasForeignKey(d => d.ReleaseApplicationID)
                .HasConstraintName("FK_DetainedLicenses_Applications");

            entity.HasOne(d => d.ReleasedByUser).WithMany(p => p.DetainedLicenseReleasedByUsers)
                .HasForeignKey(d => d.ReleasedByUserID)
                .HasConstraintName("FK_DetainedLicenses_Users1");
        });

        modelBuilder.Entity<DetainedLicenses_View>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DetainedLicenses_View");

            entity.Property(e => e.DetainDate).HasColumnType("smalldatetime");
            entity.Property(e => e.FineFees).HasColumnType("smallmoney");
            entity.Property(e => e.FullName).HasMaxLength(83);
            entity.Property(e => e.NationalNo).HasMaxLength(20);
            entity.Property(e => e.ReleaseDate).HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverID).HasName("PK_Drivers_1");

            entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_Users");

            entity.HasOne(d => d.Person).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_People");
        });

        modelBuilder.Entity<Drivers_View>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Drivers_View");

            entity.Property(e => e.CreatedDate).HasColumnType("smalldatetime");
            entity.Property(e => e.FullName).HasMaxLength(83);
            entity.Property(e => e.NationalNo).HasMaxLength(20);
        });

        modelBuilder.Entity<InternationalLicense>(entity =>
        {
            entity.Property(e => e.ExpirationDate).HasColumnType("smalldatetime");
            entity.Property(e => e.IssueDate).HasColumnType("smalldatetime");

            entity.HasOne(d => d.Application).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.ApplicationID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Applications");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Users");

            entity.HasOne(d => d.Driver).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.DriverID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Drivers");

            entity.HasOne(d => d.IssuedUsingLocalLicense).WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.IssuedUsingLocalLicenseID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Licenses");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_ArchiveDeletedLicenses"));

            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.IssueReason)
                .HasDefaultValue((byte)1)
                .HasComment("1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");

            entity.HasOne(d => d.Application).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.ApplicationID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Applications");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Users");

            entity.HasOne(d => d.Driver).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.DriverID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Drivers");

            entity.HasOne(d => d.LicenseClassNavigation).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.LicenseClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_LicenseClasses");
        });

        modelBuilder.Entity<LicenseClass>(entity =>
        {
            entity.Property(e => e.ClassDescription).HasMaxLength(500);
            entity.Property(e => e.ClassFees).HasColumnType("smallmoney");
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.DefaultValidityLength)
                .HasDefaultValue((byte)1)
                .HasComment("How many years the licesnse will be valid.");
            entity.Property(e => e.MinimumAllowedAge)
                .HasDefaultValue((byte)18)
                .HasComment("Minmum age allowed to apply for this license");
        });

        modelBuilder.Entity<LocalDrivingLicenseApplication>(entity =>
        {
            entity.HasKey(e => e.LocalDrivingLicenseApplicationID).HasName("PK_DrivingLicsenseApplications");

            entity.HasOne(d => d.Application).WithMany(p => p.LocalDrivingLicenseApplications)
                .HasForeignKey(d => d.ApplicationID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DrivingLicsenseApplications_Applications");

            entity.HasOne(d => d.LicenseClass).WithMany(p => p.LocalDrivingLicenseApplications)
                .HasForeignKey(d => d.LicenseClassID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DrivingLicsenseApplications_LicenseClasses");
        });

        modelBuilder.Entity<LocalDrivingLicenseApplications_View>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LocalDrivingLicenseApplications_View");

            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(83);
            entity.Property(e => e.NationalNo).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LocalDrivingLicenseFullApplications_View>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LocalDrivingLicenseFullApplications_View");

            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.LastStatusDate).HasColumnType("datetime");
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ExceptionType).HasMaxLength(255);
            entity.Property(e => e.HttpMethod)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Level)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Message).HasMaxLength(1000);
            entity.Property(e => e.RequestPath).HasMaxLength(500);
        });

        modelBuilder.Entity<Massage>(entity =>
        {
            entity.HasKey(e => e.MessageID).HasName("PK__Massage__C87C037CB528BBA3");

            entity.ToTable("Massage");

            entity.Property(e => e.MessageContent).HasMaxLength(3000);
            entity.Property(e => e.MessageTitle).HasMaxLength(250);

            entity.HasOne(d => d.Person).WithMany(p => p.Massages)
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Massage__PersonI__31A25463");
        });

        modelBuilder.Entity<OldUserNamesLog>(entity =>
        {
            entity.HasKey(e => e.LogID).HasName("PK__OldUserN__5E5499A8D4F3ED41");

            entity.ToTable("OldUserNamesLog");

            entity.Property(e => e.NewUserName).HasMaxLength(50);
            entity.Property(e => e.OldUserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_OnUserNameUpdated"));

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.Gender)
                .HasDefaultValue((byte)0)
                .HasComment("0 Male , 1 Femail");
            entity.Property(e => e.ImagePath).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.NationalNo).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(120);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.SecondName).HasMaxLength(20);
            entity.Property(e => e.ThirdName).HasMaxLength(20);
            entity.Property(e => e.UserName)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(e => e.TaskID).HasName("PK__Tasks__7C6949D13575F7B8");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.TaskTitle).HasMaxLength(40);

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__UserID__668030F6");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.TestResult).HasComment("0 - Fail 1-Pass");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_Users");

            entity.HasOne(d => d.TestAppointment).WithMany(p => p.Tests)
                .HasForeignKey(d => d.TestAppointmentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_TestAppointments");
        });

        modelBuilder.Entity<TestAppointment>(entity =>
        {
            entity.Property(e => e.AppointmentDate).HasColumnType("smalldatetime");
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.CreatedByUserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestAppointments_Users");

            entity.HasOne(d => d.LocalDrivingLicenseApplication).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.LocalDrivingLicenseApplicationID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestAppointments_LocalDrivingLicenseApplications");

            entity.HasOne(d => d.RetakeTestApplication).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.RetakeTestApplicationID)
                .HasConstraintName("FK_TestAppointments_Applications");

            entity.HasOne(d => d.TestType).WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.TestTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TestAppointments_TestTypes");
        });

        modelBuilder.Entity<TestAppointments_View>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TestAppointments_View");

            entity.Property(e => e.AppointmentDate).HasColumnType("smalldatetime");
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(83);
            entity.Property(e => e.PaidFees).HasColumnType("smallmoney");
            entity.Property(e => e.TestTypeTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<TestType>(entity =>
        {
            entity.Property(e => e.TestTypeDescription).HasMaxLength(500);
            entity.Property(e => e.TestTypeFees).HasColumnType("smallmoney");
            entity.Property(e => e.TestTypeTitle).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_InsteadOfDeleteStudent"));

            entity.Property(e => e.Password).HasMaxLength(70);
            entity.Property(e => e.UserName).HasMaxLength(20);

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_People");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
