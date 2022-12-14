using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Employees;
using Schoolager.Web.Models.Lessons;
using Schoolager.Web.Models.Students;
using Schoolager.Web.Models.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schoolager.Web.Helpers
{
    public interface IConverterHelper
    {
        Teacher ToTeacher(TeacherViewModel model, Guid imageId, bool isNew);
        TeacherViewModel ToTeacherViewModel(Teacher teacher);
        Student ToStudent(StudentViewModel model, Guid imageId, bool isNew);
        Student ToStudent(StudentViewModel model, int? turmaId);
        StudentViewModel ToStudentViewModel(Student student);
        List<StudentViewModel> AllToStudentViewModel(List<Student> students);
        List<Student> AllToStudent(List<StudentViewModel> model, int? turmaId);

        Lesson ToLesson(LessonViewModel model, bool isNew);
        LessonViewModel ToLessonViewModel(Lesson lesson);
        ICollection<LessonViewModel> AllToLessonViewModel(IQueryable lessons);
        User ToUser(IIsUser userEntity, User user, string blobContainerName);
        EmployeeViewModel ToEmployeeViewModel(User user);
        List<Lesson> AllToLesson(List<Lesson> lessons, bool v);
    }
}