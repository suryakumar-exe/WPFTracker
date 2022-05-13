﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WPFProjectTracker.DB
{
    public partial class PERSONALTRACKINGContext : DbContext
    {
        public PERSONALTRACKINGContext()
        {
        }

        public PERSONALTRACKINGContext(DbContextOptions<PERSONALTRACKINGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Permissionstate> Permissionstates { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Salarymonth> Salarymonths { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Taskstate> Taskstates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.; Database=PERSONAL TRACKING; trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_DEPARTMENT");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_POSITION");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("PERMISSION");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.PermissionEndDate).HasColumnType("date");

                entity.Property(e => e.PermissionExplanation).IsUnicode(false);

                entity.Property(e => e.PermissionStartDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERMISSION_EMPLOYEE");

                entity.HasOne(d => d.PermissionStateNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.PermissionState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERMISSION_PERMISSIONSTATE");
            });

            modelBuilder.Entity<Permissionstate>(entity =>
            {
                entity.ToTable("PERMISSIONSTATE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("POSITION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POSITION_DEPARTMENT");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("SALARY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_SALARY_EMPLOYEE");

                entity.HasOne(d => d.MonthNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.Month)
                    .HasConstraintName("FK_SALARY_SALARYMONTHS");
            });

            modelBuilder.Entity<Salarymonth>(entity =>
            {
                entity.ToTable("SALARYMONTHS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MonthName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("TASK");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.TaskContent).IsUnicode(false);

                entity.Property(e => e.TaskDeliveryDate).HasColumnType("date");

                entity.Property(e => e.TaskStartDate).HasColumnType("date");

                entity.Property(e => e.TaskTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TaskStateNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TaskState)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TASK_TASKSTATE");
            });

            modelBuilder.Entity<Taskstate>(entity =>
            {
                entity.ToTable("TASKSTATE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
