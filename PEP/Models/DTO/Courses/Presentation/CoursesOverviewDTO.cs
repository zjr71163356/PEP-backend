namespace PEP.Models.DTO.Courses.Presentation
{
    public class CoursesOverviewDTO
    {

        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string Introduction { get; set; } = null!;

        public string? ImageUrl { get; set; }


        public virtual ICollection<CoursesTagDTO> CourseTags { get; set; } = new List<CoursesTagDTO>();


    }
}
