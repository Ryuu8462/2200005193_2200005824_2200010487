using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lunar_Veil.Models;

public partial class _22bitv02EmployeeContext : DbContext
{
    public _22bitv02EmployeeContext()
    {
    }

    public _22bitv02EmployeeContext(DbContextOptions<_22bitv02EmployeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-BFT57FA;Initial Catalog=22BITV02_Employee;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED9BF886D3");

            entity.ToTable("Department");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.OfficePhone).HasMaxLength(20);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1149A0D1B0");

            entity.ToTable("Employee");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PhotoImagePath).HasMaxLength(255);
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Department).WithMany(p => Employee(p))
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Depart__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static object Employee(string p) => GetEmployee(p);

    private static object GetEmployee(string p)
    {
        return p.Employee;
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
