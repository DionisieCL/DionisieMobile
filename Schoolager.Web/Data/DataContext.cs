using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;

namespace Schoolager.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SubjectTurma> SubjectTurmas { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonData> LessonDatas { get; set; }
        public DbSet<StudentLessonData> StudentLessonsData { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>(builder =>
            {
                builder.HasMany(s => s.Teachers)
                .WithOne(t => t.Subject)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.StudentId, g.SubjectId });

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(sub => sub.Grades)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubjectTurma>()
                .HasKey(st => new { st.SubjectId, st.TurmaId });

            modelBuilder.Entity<SubjectTurma>()
                .HasOne(st => st.Turma)
                .WithMany(t => t.SubjectTurma)
                .HasForeignKey(st => st.TurmaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubjectTurma>()
                .HasOne(st => st.Subject)
                .WithMany(sub => sub.SubjectTurma)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Turma)
                .WithMany(t => t.Students)
                .HasForeignKey(s => s.TurmaId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<StudentLessonData>()
                .HasKey(sl => new { sl.StudentId, sl.LessonDataId });

            modelBuilder.Entity<StudentLessonData>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.StudentLessonsData)
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentLessonData>()
                .HasOne(sl => sl.LessonData)
                .WithMany(s => s.StudentLessonsData)
                .HasForeignKey(l => l.LessonDataId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
