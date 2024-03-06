namespace PEP.Models.DTO.Courses.Presentation
{
    public class PreCoursesChapterDTO
    {
        public int ChapterId { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; } = null!;

        public int ChapterNumber { get; set; }


        public virtual ICollection<PreCoursesSubChapterDTO> SubChapters { get; set; } = new List<PreCoursesSubChapterDTO>();
    }
}
