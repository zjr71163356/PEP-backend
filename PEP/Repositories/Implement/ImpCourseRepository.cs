using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.Domain;
using PEP.Repositories.Interface;

namespace PEP.Repositories.Implement
{
    public class ImpCourseRepository : ICourseRepository
    {
        private readonly FinalDesignContext dbContext;

        public ImpCourseRepository(FinalDesignContext dbContext)
        {
            this.dbContext = dbContext;
        }


 
        public async Task<Course> AddCourseAsync(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
            return course;
        }



        public async Task<Course?> DeleteCourseByIdAsync(int courseId)
        {
            var course = await dbContext.Courses
            .Include(c => c.UserCourses)
            .Include(c => c.CourseTags)
            .Include(c => c.CourseChapters)
            .ThenInclude(cc => cc.SubChapters)
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
            {
                return null;
            }

            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

 

        public async Task<List<Course>> GetAllCoursesListAsync()
        {
            var allcourses = await dbContext.Courses.Include(c => c.CourseTags).ToListAsync();
            return allcourses;
        }

        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.CourseTags)
                .Include(c => c.CourseChapters)
                .ThenInclude(cc => cc.SubChapters)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (course == null)
                return null;

            return course;
        }

        public Task<List<Course>> GetUserCoursesListAsync(int userId)
        {
            throw new NotImplementedException();
        }

 

        public async Task<Course?> UpdateCourseStepOneAsync(int courseId, Course course)
        {
            var existingCourse = await dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (existingCourse == null)
            {
                return null;
            }
            existingCourse.CourseName = course.CourseName;
            existingCourse.ChapterCount = course.ChapterCount;
            existingCourse.ImageUrl = course.ImageUrl;
            existingCourse.Introduction = course.Introduction;
            existingCourse.CourseTags = course.CourseTags;


            await dbContext.SaveChangesAsync();
            return existingCourse;
        }

        public async Task<Course?> UpdateCourseStepTwoAsync(int courseId, Course course)
        {
            var existingCourse = await dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (existingCourse == null)
            {
                return null;
            }

            existingCourse.CourseChapters = course.CourseChapters;

            await dbContext.SaveChangesAsync();
            return existingCourse;
        }
    }
}
