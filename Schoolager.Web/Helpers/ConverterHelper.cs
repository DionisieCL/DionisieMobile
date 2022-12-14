using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Employees;
using Schoolager.Web.Models.Lessons;
using Schoolager.Web.Models.Students;
using Schoolager.Web.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                ImageId = imageId,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Subject = model.Subject,
                SubjectId = model.SubjectId,
                User = model.User,
                UserId = model.UserId,
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
                ImageId = teacher.ImageId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                User = teacher.User,
                UserId = teacher.UserId,
                Subject = teacher.Subject,
                SubjectId = teacher.SubjectId,
            };
        }

        public Student ToStudent(StudentViewModel model, Guid imageId, bool isNew)
        {
            var student = new Student
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Turma = model.Turma,
                TurmaId = model.TurmaId,
                Grades = model.Grades,
                UserId = model.UserId,
                SchoolYear = model.SchoolYear,
            };

            return student;
        }

        public Student ToStudent(StudentViewModel model, int? turmaId)
        {
            var student = new Student
            {
                Id = model.Id,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Turma = model.Turma,
                TurmaId = turmaId,
                Grades = model.Grades,
                User = model.User,
                ImageId = model.ImageId,
                SchoolYear = model.SchoolYear,
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
                ImageId = student.ImageId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                User = student.User,
                Turma = student.Turma,
                TurmaId = student.TurmaId,
                Grades = student.Grades,
                UserId = student.UserId,
                SchoolYear = student.SchoolYear
            };
        }

        public List<StudentViewModel> AllToStudentViewModel(List<Student> students)
        {
            List<StudentViewModel> model = new List<StudentViewModel>();

            foreach (Student student in students)
            {
                model.Add(ToStudentViewModel(student));
            }

            return model;
        }


        public List<Student> AllToStudent(List<StudentViewModel> model, int? turmaId)
        {
            List<Student> students = new List<Student>();

            foreach(var item in model)
            {
                students.Add(ToStudent(item, turmaId));
            }

            return students;
        }

        public Lesson ToLesson(LessonViewModel model, bool isNew)
        {
            TimeSpan startTime = TimeSpan.Parse(model.StartTimeString);
            TimeSpan endTime = TimeSpan.Parse(model.EndTimeString);

            DateTime firstDay = model.StartTime.Value.StartOfWeek(DayOfWeek.Monday);

            model.StartTime = firstDay;
            model.EndTime = firstDay;

            int dayDifference = model.WeekDay - (int)firstDay.DayOfWeek;

            model.StartTime =  model.StartTime.Value.AddDays(dayDifference);
            model.EndTime = model.EndTime.Value.AddDays(dayDifference);

            model.StartTime = model.StartTime.Value.Date + startTime;
            model.EndTime = model.EndTime.Value.Date + endTime;

            return new Lesson
            {
                Id = isNew ? 0 : model.Id,
                SubjectName = model.SubjectName,
                SubjectId = model.SubjectId,
                TeacherId = model.TeacherId,
                TurmaId = model.TurmaId,
                StartTime = model.StartTime.Value,
                EndTime = model.EndTime.Value,
                //Location = model.Location,
                RoomId = model.RoomId,
                RecurrenceRule = model.RecurrenceRule,
                RecurrenceException = model.RecurrenceException,
                WeekDay = model.WeekDay,
            };
        }

        public LessonViewModel ToLessonViewModel(Lesson lesson)
        {
            string startTimeString = lesson.StartTime.Value.ToString("h:mm");
            string endTimeString = lesson.EndTime.Value.ToString("h:mm");

            return new LessonViewModel
            {
                Id = lesson.Id,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                SubjectId = lesson.SubjectId,
                TeacherId = lesson.SubjectId,
                TurmaId = lesson.TurmaId,
                SubjectName = lesson.SubjectName,
                //Location = lesson.Location == null ? "" : lesson.Location,
                RoomId = lesson.RoomId,
                RecurrenceRule = lesson.RecurrenceRule,
                RecurrenceException = lesson.RecurrenceException,
                WeekDay = lesson.WeekDay,
                StartTimeString = startTimeString,
                EndTimeString = endTimeString,
            };
        }

        public ICollection<LessonViewModel> AllToLessonViewModel(IQueryable lessons)
        {
            List<LessonViewModel> lessonViewModels = new List<LessonViewModel>();

            foreach(Lesson lesson in lessons)
            {
                lessonViewModels.Add(ToLessonViewModel(lesson));
            }

            return lessonViewModels;
        }

        public User ToUser(IIsUser userEntity, User user, string blobContainerName)
        {
            user.FirstName = userEntity.FirstName;
            user.LastName = userEntity.LastName;
            user.UserName = userEntity.Email;
            user.Email = userEntity.Email;
            //user.EmailConfirmed = false;
            user.PhoneNumber = userEntity.PhoneNumber;
            user.BlobContainer = blobContainerName;
            user.ImageId = userEntity.ImageId;

            return user;
        }
    
        public EmployeeViewModel ToEmployeeViewModel(User user)
        {
            return new EmployeeViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ImageId = user.ImageId,
                UserName = user.Email,
                IsAdmin = user.IsAdmin,
                BlobContainer = user.BlobContainer,
            };
        }

    }
}
