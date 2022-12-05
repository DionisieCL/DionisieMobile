using Schoolager.Web.Models.Students;

namespace Schoolager.Web.Models.Grades
{
    public class GradeViewModel
    {
        public StudentViewModel StudentViewModel { get; set; }

        public double FirstTermMark { get; set; }
        public double SecondTermMark { get; set; }
        public double ThirdTermMark { get; set; }
        public int StudentId { get; set; }
    }
}
