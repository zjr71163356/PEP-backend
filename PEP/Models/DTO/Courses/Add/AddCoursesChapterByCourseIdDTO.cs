namespace PEP.Models.DTO.Courses.Add
{
    public class AddCoursesChapterByCourseIdDTO
    {

        public int CourseId { get; set; }
        public string Title { get; set; } = null!;

        public int ChapterNumber { get; set; }

 
    }
}
