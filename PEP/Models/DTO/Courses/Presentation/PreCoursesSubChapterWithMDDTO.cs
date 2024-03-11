namespace PEP.Models.DTO.Courses.Presentation
{
    public class PreCoursesSubChapterWithMDDTO
    {
        public int SubChapterId { get; set; }

        public int ParentChapterId { get; set; }


        public decimal SubChapterNumber { get; set; }

        public string Title { get; set; } = null!;
        public string MarkdownContent { get; set; } = null!;
    }
}
