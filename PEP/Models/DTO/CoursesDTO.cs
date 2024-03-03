using PEP.Model.Domain;

namespace PEP.Models.DTO
{
    public class CoursesDTO
    {
 

        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string? Introduction { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<CourseChapter> CourseChapters { get; set; } = new List<CourseChapter>();

        public virtual ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();

        public virtual ICollection<SubChapter> SubChapters { get; set; } = new List<SubChapter>();

        public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    }
}
