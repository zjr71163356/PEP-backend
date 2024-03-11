using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Presentation;

namespace PEP.Models.DTO.Courses.Both
{
    public class CoursesStepTwoDTO
    {

        public virtual ICollection<PreCoursesChapterDTO> CourseChapters { get; set; } = new List<PreCoursesChapterDTO>();


    }
}
