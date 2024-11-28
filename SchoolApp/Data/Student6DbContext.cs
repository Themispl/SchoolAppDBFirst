using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Data;

public partial class Student6DbContext : DbContext
{
    public Student6DbContext()
    {
    }

    public Student6DbContext(DbContextOptions<Student6DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC072D856853");

            entity.HasIndex(e => e.Descreption, "IX_Courses_Description");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descreption)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK_Courses_Teachers");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07EEA700F0");

            entity.HasIndex(e => e.Am, "IX_Stubents_AM").IsUnique();

            entity.HasIndex(e => e.UserId, "IX_Stubents_UserId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Am)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("AM");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Institution)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursesStudent",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CoursesStudents_CourseId"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CoursesStudents_StudentId"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId").HasName("PK_Table");
                        j.ToTable("CoursesStudents");
                    });
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC07C0C99ABF");

            entity.ToTable("Table");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teachers__3214EC0790583FE7");

            entity.HasIndex(e => e.Institution, "IX_Teachers_Intitution");

            entity.HasIndex(e => e.UserId, "IX_Teachers_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Institution)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07D15D0F09");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IX_User_Email");

            entity.HasIndex(e => e.Username, "IX_User_Username");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UserRole)
                .HasConversion<string>()
                .HasMaxLength(20);
                
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
