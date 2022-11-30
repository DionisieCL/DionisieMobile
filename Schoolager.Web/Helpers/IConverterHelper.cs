using Schoolager.Web.Data.Entities;
using Schoolager.Web.Models.Lessons;
using Schoolager.Web.Models.Students;
using Schoolager.Web.Models.Teachers;
using System;

namespace Schoolager.Web.Helpers
{
    public interface IConverterHelper
    {
        Teacher ToTeacher(TeacherViewModel model, Guid imageId, bool isNew);
        TeacherViewModel ToTeacherViewModel(Teacher teacher);

        Student ToStudent(StudentViewModel model, Guid imageId, bool isNew);
        StudentViewModel ToStudentViewModel(Student student);

        Lesson ToLesson(LessonViewModel model, bool isNew);
        LessonViewModel ToLessonViewModel(Lesson lesson);
    }
}