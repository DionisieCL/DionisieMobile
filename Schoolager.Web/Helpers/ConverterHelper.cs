using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Students;
using Schoolager.Web.Models.Teachers;
using System;
using System.Threading.Tasks;

namespace Schoolager.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Teacher ToTeacher(TeacherViewModel model, Guid imageId, bool isNew)
        {
            var teacher = new Teacher
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = imageId,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Subject = model.Subject,
                SubjectId = model.SubjectId,
                User = model.User,
            };

            return teacher;
        }

        public TeacherViewModel ToTeacherViewModel(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                Address = teacher.Address,
                DateOfBirth = teacher.DateOfBirth,
                Email = teacher.Email,
                ImageUrl = teacher.ImageUrl,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                User = teacher.User,
                Subject = teacher.Subject,
                SubjectId = teacher.SubjectId,
            };
        }

        public Student ToStudent(StudentViewModel model, Guid imageId, bool isNew)
        {
            var student = new Student
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = imageId,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Turma = model.Turma,
                TurmaId = model.TurmaId,
                Grades = model.Grades,
                User = model.User,
            };

            return student;
        }

        public StudentViewModel ToStudentViewModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                ImageUrl = student.ImageUrl,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                User = student.User,
                Turma = student.Turma,
                TurmaId = student.TurmaId,
                Grades = student.Grades,
            };
        }

    }
}
