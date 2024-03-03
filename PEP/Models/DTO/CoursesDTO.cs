using PEP.Model.Domain;

namespace PEP.Models.DTO
{
    public class CoursesIntroDTO
    {
 

        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string? Introduction { get; set; }

        public string? ImageUrl { get; set; }


        public virtual ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();

 
    }
}
