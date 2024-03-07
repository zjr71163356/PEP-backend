using PEP.Models.Domain;

namespace PEP.Repositories.Interface
{
    public interface ICourseRepository
    {
        //teacher/admin manage all the courses
        Task<List<Course>> GetAllCoursesListAsync();
        Task<Course?> GetCourseByIdAsync(int courseId);

        Task<Course> AddCourseAsync(Course course);

        Task<Course?> DeleteCourseByIdAsync(int courseId);
        Task<Course?> UpdateCourseStepOneAsync(int courseId,Course course);

        Task<Course?> UpdateCourseStepTwoAsync(int courseId,Course course);
 



        //user manage user's courses
        Task<List<Course>> GetUserCoursesListAsync(int userId);
        Task<SubChapter?> GetSubChapterById(int subChapterId);
    }
}
