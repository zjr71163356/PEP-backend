namespace PEP.Models.DTO.Courses
{
    public class CoursesSubChapterDTO
    {

        public int SubChapterId { get; set; }

        public int CourseId { get; set; }


        public decimal SubChapterNumber { get; set; }

        public string Title { get; set; } = null!;

        public string MarkdownContent { get; set; } = null!;
    }
}
