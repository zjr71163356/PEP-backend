using PEP.Models.DTO.Courses.Add;

namespace PEP.Models.DTO.Courses.Both
{
    public class CoursesStepTwoDTO
    {

        public virtual ICollection<AddCoursesChapterDTO> CourseChapters { get; set; } = new List<AddCoursesChapterDTO>();


    }
}
