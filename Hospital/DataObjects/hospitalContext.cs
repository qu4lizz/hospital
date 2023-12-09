using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hospital.DataObjects
{
    public partial class hospitalContext : DbContext
    {
        public hospitalContext()
        {
        }

        public hospitalContext(DbContextOptions<hospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admission> Admissions { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Nurse> Nurses { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<Surgery> Surgeries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["HospitalDatabase"].ConnectionString;

                if (String.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string not found in App.config");
                }

                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admission>(entity =>
            {
                entity.ToTable("admission");

                entity.HasIndex(e => e.PatientId, "fk_Admission_Patient1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdmissionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("admission_date");

                entity.Property(e => e.DischargeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("discharge_date");

                entity.Property(e => e.PatientId).HasColumnName("Patient_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Admissions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Admission_Patient1");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasIndex(e => e.DoctorId, "fk_Appointment_Doctor_idx");

                entity.HasIndex(e => e.PatientId, "fk_Appointment_Patient1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_idDoctor");

                entity.Property(e => e.PatientId).HasColumnName("Patient_idPatient");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Appointment_Doctor");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Appointment_Patient1");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctor");

                entity.HasIndex(e => e.Username, "username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contact)
                    .HasMaxLength(15)
                    .HasColumnName("contact");

                entity.Property(e => e.Language)
                    .HasMaxLength(5)
                    .HasColumnName("language");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.Property(e => e.Theme)
                    .HasMaxLength(10)
                    .HasColumnName("theme");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(45)
                    .HasColumnName("item_name");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("manager");

                entity.HasIndex(e => e.Username, "username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contact)
                    .HasMaxLength(45)
                    .HasColumnName("contact");

                entity.Property(e => e.Language)
                    .HasMaxLength(5)
                    .HasColumnName("language");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.Surname)
                    .HasMaxLength(45)
                    .HasColumnName("surname");

                entity.Property(e => e.Theme)
                    .HasMaxLength(10)
                    .HasColumnName("theme");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.ToTable("nurse");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contact)
                    .HasMaxLength(45)
                    .HasColumnName("contact");

                entity.Property(e => e.Language)
                    .HasMaxLength(45)
                    .HasColumnName("language");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");

                entity.Property(e => e.Surname)
                    .HasMaxLength(45)
                    .HasColumnName("surname");

                entity.Property(e => e.Theme)
                    .HasMaxLength(45)
                    .HasColumnName("theme");

                entity.Property(e => e.Username)
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Contact)
                    .HasMaxLength(15)
                    .HasColumnName("contact");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("record");

                entity.HasIndex(e => e.DoctorId, "fk_Record_Doctor1_idx");

                entity.HasIndex(e => e.PatientId, "fk_Record_Patient1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Diagnosis)
                    .HasMaxLength(200)
                    .HasColumnName("diagnosis");

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_idDoctor");

                entity.Property(e => e.PatientId).HasColumnName("Patient_idPatient");

                entity.Property(e => e.Prescription)
                    .HasMaxLength(200)
                    .HasColumnName("prescription");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Record_Doctor1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Record_Patient1");
            });

            modelBuilder.Entity<Surgery>(entity =>
            {
                entity.HasKey(e => e.IdSurgery)
                    .HasName("PRIMARY");

                entity.ToTable("surgery");

                entity.HasIndex(e => e.DoctorId, "fk_Surgery_Doctor1_idx");

                entity.HasIndex(e => e.PatientId, "fk_Surgery_Patient1_idx");

                entity.Property(e => e.IdSurgery).HasColumnName("idSurgery");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_id");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .HasColumnName("notes");

                entity.Property(e => e.PatientId).HasColumnName("Patient_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Surgeries)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Surgery_Doctor1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Surgeries)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Surgery_Patient1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
