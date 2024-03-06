using PEP.Models.Domain;
using PEP.Models.DTO.Courses.Both;

namespace PEP.Models.DTO.Courses.Presentation
{
    public class CourseDescDTO
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string Introduction { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public virtual ICollection<PreCoursesChapterDTO> CourseChapters { get; set; } = new List<PreCoursesChapterDTO>();

        public virtual ICollection<CoursesTagDTO> CourseTags { get; set; } = new List<CoursesTagDTO>();
    }
}
