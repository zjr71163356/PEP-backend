using PEP.Model.Domain;

namespace PEP.Models.DTO.Courses
{
    public class CoursesChapterDTO
    {
        public int ChapterId { get; set; }

        public int? CourseId { get; set; }

        public string Title { get; set; } = null!;

        public int ChapterNumber { get; set; }


        public virtual ICollection<CoursesSubChapterDTO> SubChapters { get; set; } = new List<CoursesSubChapterDTO>();
    }
}
