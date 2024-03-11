using Microsoft.AspNetCore.Mvc;
using PEP.Models.Domain;

namespace PEP.Repositories.Interface
{
    public interface ICourseRepository
    {
        //teacher/admin manage all the courses
        Task<List<Course>> GetAllCoursesListAsync( string? fitlerQuery,   int pageNumber, int? pageSize);
        Task<Course?> GetCourseByIdAsync(int courseId);

        Task<Course> AddCourseAsync(Course course);

        Task<Course?> DeleteCourseByIdAsync(int courseId);
        Task<Course?> UpdateCourseStepOneAsync(int courseId,Course course);

        Task<Course?> UpdateCourseStepTwoAsync(int courseId,Course course);
        



        //user manage user's courses
        Task<List<Course>> GetUserCoursesListAsync(int userId);


        //teacher/admin manage course's chapters

        Task<CourseChapter?> GetChapterById(int chapterId);
        Task<CourseChapter?> UpdateChapter(int chapterId,CourseChapter chapter);
        Task<CourseChapter?> AddChapter(CourseChapter chapter);
        Task<CourseChapter?> DeleteChapterById(int chapter);


        //teacher/admin manage course's subchapters
        Task<SubChapter?> GetSubChapterById(int subChapterId);
        Task<SubChapter?> UpdateSubChapter(int subChapterId,SubChapter subChapter);
        Task<SubChapter?> UpdateSubChapterMDcontent(int subChapterId, SubChapter subChapter);
        Task<SubChapter?> AddSubChapter(SubChapter subChapter);

        Task<SubChapter?> DeleteSubChapterById(int subChapterId);

    }
}
