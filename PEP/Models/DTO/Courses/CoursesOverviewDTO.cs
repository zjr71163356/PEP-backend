using PEP.Model.Domain;

namespace PEP.Models.DTO.Courses
{
    public class CoursesOverviewDTO
    {


        public string CourseName { get; set; } = null!;

        public int ChapterCount { get; set; }

        public string? Introduction { get; set; }

        public string? ImageUrl { get; set; }


        public virtual ICollection<CoursesTagDTO> CourseTags { get; set; } = new List<CoursesTagDTO>();


    }
}
