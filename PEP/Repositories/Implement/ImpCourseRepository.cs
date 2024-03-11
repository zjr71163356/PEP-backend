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

        public async Task<CourseChapter?> AddChapter(CourseChapter chapter)
        {
            await dbContext.CourseChapters.AddAsync(chapter);
            await dbContext.SaveChangesAsync();
            return chapter;
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

        public async Task<CourseChapter?> DeleteChapterById(int chapter)
        {
            var existingCourseChapter = await dbContext.CourseChapters.Include(cc=>cc.SubChapters).FirstOrDefaultAsync(cc => cc.ChapterId == chapter);
            if (existingCourseChapter == null)
            {
                return null;
            }
            dbContext.CourseChapters.Remove(existingCourseChapter);
            await dbContext.SaveChangesAsync();
            return existingCourseChapter;
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

        public async Task<List<Course>> GetAllCoursesListAsync(string? fitlerQuery, int pageNumber, int? pageSize)
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

        public async Task<CourseChapter?> GetChapterById(int chapterId)
        {
        var existingCourseChapter =await dbContext.CourseChapters.FirstOrDefaultAsync(cc => cc.ChapterId == chapterId);
            if (existingCourseChapter == null)
            {
            return null;
            }

            return existingCourseChapter;
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

        public async Task<CourseChapter?> UpdateChapter(int chapterId, CourseChapter chapter)
        {
         var existingCourseChapter =await dbContext.CourseChapters.Include(cc=>cc.SubChapters).FirstOrDefaultAsync(cc=>cc.ChapterId== chapterId);
            if (existingCourseChapter == null)
            {
                return null;
            }
            existingCourseChapter.ChapterNumber = chapter.ChapterNumber;
            existingCourseChapter.Title = chapter.Title;
            existingCourseChapter.SubChapters = chapter.SubChapters;
            await dbContext.SaveChangesAsync();
            return existingCourseChapter;
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

        public async Task<Course?> UpdateCourseStepTwoAsync(int courseId, Course updatedCourse)
        {
            var existingCourse = await dbContext.Courses
               .Include(c => c.CourseChapters)
                   .ThenInclude(cc => cc.SubChapters)
               .FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (existingCourse == null)
            {
                return null;
            }

        
            // 遍历更新 CourseChapter 的 title 字段
            foreach (var updatedChapter in updatedCourse.CourseChapters)
            {
                var existingChapter = existingCourse.CourseChapters.FirstOrDefault(cc => cc.ChapterId == updatedChapter.ChapterId);

                if (existingChapter != null)
                {
                    existingChapter.Title = updatedChapter.Title;

                    // 遍历更新 SubChapter 的 title 字段
                    foreach (var updatedSubChapter in updatedChapter.SubChapters)
                    {
                        var existingSubChapter = existingChapter.SubChapters.FirstOrDefault(sc => sc.SubChapterId == updatedSubChapter.SubChapterId);

                        if (existingSubChapter != null)
                        {
                            existingSubChapter.Title = updatedSubChapter.Title;
                        }
                    }
                }
            }

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

        public async Task<SubChapter?> UpdateSubChapterMDcontent(int subChapterId, SubChapter subChapter)
        {
            var existingSubChapter = await dbContext.SubChapters.FirstOrDefaultAsync(c => c.SubChapterId == subChapterId);
            if (existingSubChapter == null)
                return null;
            existingSubChapter.MarkdownContent = subChapter.MarkdownContent;
            await dbContext.SaveChangesAsync();
            return existingSubChapter;
        }
    }
}
