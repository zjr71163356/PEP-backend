using PEP.Models.DTO.Courses.Presentation;

namespace PEP.Models.DTO.Courses.Add
{
    public class AddCoursesChapterDTO
    {

 

        public string Title { get; set; } = null!;

        public int ChapterNumber { get; set; }


        public virtual ICollection<AddCoursesSubChapterDTO> SubChapters { get; set; } = new List<AddCoursesSubChapterDTO>();
    }
}
