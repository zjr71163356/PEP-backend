using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<SubChapter?> AddSubChapter(SubChapter subChapter)
        {
            await dbContext.SubChapters.AddAsync(subChapter);
            await dbContext.SaveChangesAsync();

            return subChapter;
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

        public async Task<SubChapter?> DeleteSubChapterById(int subChapterId)
        {
            var subChapter = await dbContext.SubChapters.FirstOrDefaultAsync(s => s.SubChapterId == subChapterId);
            if (subChapter == null)
            {
                return null;
            }
            dbContext.SubChapters.Remove(subChapter);
            await dbContext.SaveChangesAsync();
            return subChapter;
        }

        public async Task<List<Course>> GetAllCoursesListAsync([FromQuery] string? fitlerQuery, [FromQuery] int pageNumber, [FromQuery] int? pageSize)
        {

            var allQueryCourses = dbContext.Courses.Include(c => c.CourseTags).AsQueryable();


            if (!string.IsNullOrEmpty(fitlerQuery))
            {
                fitlerQuery = fitlerQuery.Trim();
                allQueryCourses = allQueryCourses.Where(c => c.CourseName.Contains(fitlerQuery));
            }

            if (pageSize == null)
            {
                return await allQueryCourses.ToListAsync();
            }
            else
            {
                int skipResult = (pageNumber - 1) * pageSize.Value;
                return await allQueryCourses.Skip(skipResult).Take(pageSize.Value).ToListAsync();
            }
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

        public async Task<SubChapter?> GetSubChapterById(int subChapterId)
        {
            var subChapter = await dbContext.SubChapters.FirstOrDefaultAsync(sc => sc.SubChapterId == subChapterId);
            if (subChapter == null)
            {
                return null;
            }
            return subChapter;
        }

        public Task<List<Course>> GetUserCoursesListAsync(int userId)
        {
            throw new NotImplementedException();
        }



        public async Task<Course?> UpdateCourseStepOneAsync(int courseId, Course course)
        {
            var existingCourse = await dbContext.Courses.Include(c => c.CourseTags).FirstOrDefaultAsync(c => c.CourseId == courseId);
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

        public async Task<SubChapter?> UpdateSubChapter(int subChapterId, SubChapter subChapter)
        {

            var exsitingSubChapter = await dbContext.SubChapters.FirstOrDefaultAsync(c => c.SubChapterId == subChapterId);
            if (exsitingSubChapter == null)
            {
                return null;
            }
            exsitingSubChapter.SubChapterNumber = subChapter.SubChapterNumber;
            exsitingSubChapter.Title = subChapter.Title;
            exsitingSubChapter.MarkdownContent = subChapter.MarkdownContent;
            await dbContext.SaveChangesAsync();
            return exsitingSubChapter;
        }

    }
}
