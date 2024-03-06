namespace PEP.Models.DTO.Courses
{
    public class AddCoursesSubChapterDTO
    {

 
        public decimal SubChapterNumber { get; set; }

        public string Title { get; set; } = null!;

        public string MarkdownContent { get; set; } = null!;
    }
}
