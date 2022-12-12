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
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SubjectTurma> SubjectTurmas { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonData> LessonDatas { get; set; }
        public DbSet<StudentLessonData> StudentLessonDatas { get; set; }
        public DbSet<TeacherTurma> TeacherTurmas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<LessonResource> LessonResources  { get; set; }
        public DbSet<Doubt> Doubts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Teachers)
                .WithOne(t => t.Subject)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.StudentId, g.SubjectId });

            //modelBuilder.Entity<Grade>()
            //    .Property(g => g.Id)
            //    .ValueGeneratedOnAdd();

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(sub => sub.Grades)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SubjectTurma>()
                .HasKey(st => new { st.SubjectId, st.TurmaId });

            modelBuilder.Entity<SubjectTurma>()
                .HasOne(st => st.Turma)
                .WithMany(t => t.SubjectTurmas)
                .HasForeignKey(st => st.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubjectTurma>()
                .HasOne(st => st.Subject)
                .WithMany(sub => sub.SubjectTurma)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Student>()
                .HasOne(s => s.Turma)
                .WithMany(t => t.Students)
                .HasForeignKey(s => s.TurmaId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);


            modelBuilder.Entity<StudentLessonData>()
                .HasKey(sl => new { sl.StudentId, sl.LessonDataId });

            modelBuilder.Entity<StudentLessonData>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.StudentLessonsData)
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentLessonData>()
                .HasOne(sl => sl.LessonData)
                .WithMany(s => s.StudentLessonsData)
                .HasForeignKey(l => l.LessonDataId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<TeacherTurma>()
                .HasKey(sl => new { sl.TeacherId, sl.TurmaId });

            modelBuilder.Entity<TeacherTurma>()
                .HasOne(tt => tt.Teacher)
                .WithMany(t => t.TeacherTurmas)
                .HasForeignKey(tt => tt.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeacherTurma>()
                .HasOne(tt => tt.Turma)
                .WithMany(t => t.TeacherTurmas)
                .HasForeignKey(tt => tt.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Room)
                .WithMany(r => r.Lessons)
                .HasForeignKey(r => r.RoomId);

            //modelBuilder.Entity<Room>()
            //    .HasOne(l => l.Room)
            //    .WithMany(r => r.Lessons)
            //    .HasForeignKey(r => r.RoomId);
        }
    }
}
