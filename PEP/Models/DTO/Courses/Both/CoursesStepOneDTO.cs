using PEP.Models.Domain;

namespace PEP.Models.DTO.Courses.Both
{
    public class CoursesStepOneDTO
    {
        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string Introduction { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public virtual ICollection<CoursesTagDTO> CourseTags { get; set; } = new List<CoursesTagDTO>();
    }
}
