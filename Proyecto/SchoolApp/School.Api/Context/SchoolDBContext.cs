using Microsoft.EntityFrameworkCore;
using School.Entities;

namespace School.Api.Context
{
   public partial class SchoolDBContext : DbContext
   {
      public SchoolDBContext()
      {
      }

      public SchoolDBContext(DbContextOptions<SchoolDBContext> options)
          : base(options)
      {
      }

      public virtual DbSet<Course> Courses { get; set; } = null!;
      public virtual DbSet<CoursesStudent> CoursesStudents { get; set; } = null!;
      public virtual DbSet<Student> Students { get; set; } = null!;

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SchoolDB;Trusted_Connection=True;");
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<Course>(entity =>
         {
            entity.Property(e => e.Description).HasMaxLength(50);
         });

         modelBuilder.Entity<CoursesStudent>(entity =>
         {
            entity.HasKey(e => new { e.CourseId, e.StudentId });

            entity.HasOne(d => d.Course)
                .WithMany(p => p.CoursesStudents)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursesStudents_Courses");

            entity.HasOne(d => d.Student)
                .WithMany(p => p.CoursesStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursesStudents_Students");
         });

         modelBuilder.Entity<Student>(entity =>
         {
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

            entity.Property(e => e.FirstName).HasMaxLength(50);

            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
         });

         OnModelCreatingPartial(modelBuilder);
      }

      partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }




}