namespace PEP.Models.DTO.Courses.Add
{
    public class AddCoursesSubChapterByChapterIdDTO
    {
        public int ParentChapterId { get; set; }
        public decimal SubChapterNumber { get; set; }

        public string Title { get; set; } = null!;

        public string MarkdownContent { get; set; } = null!;
    }
}
