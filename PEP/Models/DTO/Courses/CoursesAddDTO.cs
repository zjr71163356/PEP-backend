 

namespace PEP.Models.DTO.Courses
{
    public class CoursesAddDTO
    {


        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string? Introduction { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<CoursesChapterDTO> CourseChapters { get; set; } = new List<CoursesChapterDTO>();

        public virtual ICollection<CoursesTagDTO> CourseTags { get; set; } = new List<CoursesTagDTO>();




    }
}
