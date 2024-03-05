using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;


namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly FinalDesignContext dbContext;

        public CoursesController(FinalDesignContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesList()
        {
            var allcourses = await dbContext.Courses.Include(c => c.CourseTags).ToListAsync();
            var allcoursesDTO = new List<CoursesOverviewDTO>();
            foreach (var course in allcourses)
            {
                allcoursesDTO.Add(new CoursesOverviewDTO
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    ChapterCount = course.ChapterCount,
                    Introduction = course.Introduction,
                    ImageUrl = course.ImageUrl,
                    CourseTags = course.CourseTags.Select(ct => new CoursesTagDTO
                    {
                        TagName = ct.TagName,
                        TagColor = ct.TagColor
                    }).ToList()
                });
            }
            return Ok(allcoursesDTO);
        }



        [HttpGet]
        [Route("{courseId:int}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.CourseTags)
                .Include(c => c.CourseChapters)

                .ThenInclude(cc => cc.SubChapters)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }
            var courseDTO = new CourseGetByIdDTO
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                ChapterCount = course.ChapterCount,
                Introduction = course.Introduction,
                ImageUrl = course.ImageUrl,
                CourseTags = course.CourseTags.Select(ct => new CoursesTagDTO
                {
                    TagName = ct.TagName,
                    TagColor = ct.TagColor
                }).ToList(),
                CourseChapters = course.CourseChapters.Select(cc => new CoursesChapterDTO
                {
                    ChapterId = cc.ChapterId,
                    CourseId = cc.CourseId,
                    Title = cc.Title,
                    ChapterNumber = cc.ChapterNumber,
                    SubChapters = cc.SubChapters.Select(sc => new CoursesSubChapterDTO
                    {
                        CourseId = cc.CourseId,
                        SubChapterId = sc.SubChapterId,
                        Title = sc.Title,
                        SubChapterNumber = sc.SubChapterNumber,
                        MarkdownContent = sc.MarkdownContent
                    }).ToList()
                }).ToList()
            };
            return Ok(courseDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CoursesAddDTO addCourseDTO)
        {
            foreach (var ct in addCourseDTO.CourseTags)
            {
                ct.TagColor = new Random().Next(1, 6);
            }

            var courseDomainModel = new Course
            {
                // Add properties for the new course

                CourseName = addCourseDTO.CourseName,
                ChapterCount = addCourseDTO.ChapterCount,
                Introduction = addCourseDTO.Introduction,
                ImageUrl = addCourseDTO.ImageUrl,
                CourseTags = addCourseDTO.CourseTags.Select(ct => new CourseTag
                {
                    TagName = ct.TagName,
                    TagColor = ct.TagColor
                }).ToList(),
                CourseChapters = addCourseDTO.CourseChapters.Select(cc => new CourseChapter
                {
                    Title = cc.Title,
                    ChapterNumber = cc.ChapterNumber,
                    SubChapters = cc.SubChapters.Select(sc => new SubChapter
                    {
                  
                        Title = sc.Title,
                        ParentChapterId = cc.ChapterId,
                        SubChapterNumber = sc.SubChapterNumber,
                        ParentChapterNumber = cc.ChapterNumber,
                        MarkdownContent = sc.MarkdownContent
                    }).ToList()
                }).ToList()

            };



            await dbContext.Courses.AddAsync(courseDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok( );
        }

        [HttpPut]
        [Route("StepOne/{courseId:int}")]
        public async Task<IActionResult> UpdateCourseOneStep([FromRoute] int courseId, [FromBody] CoursesUpdateOneStepDTO updateCourseOneStepDTO)
        {
            var course = await dbContext.Courses
                .Include(c => c.CourseTags)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }
            course.CourseName = updateCourseOneStepDTO.CourseName;
            course.ChapterCount = updateCourseOneStepDTO.ChapterCount;
            course.Introduction = updateCourseOneStepDTO.Introduction;
            course.ImageUrl = updateCourseOneStepDTO.ImageUrl;

            foreach (var ct in course.CourseTags)
            {
                dbContext.CourseTags.Remove(ct);
            }
            course.CourseTags.Clear();

            course.CourseTags = updateCourseOneStepDTO.CourseTags.Select(ct => new CourseTag
            {
                TagName = ct.TagName,
                TagColor = ct.TagColor
            }).ToList();



            await dbContext.SaveChangesAsync();
            return Ok(updateCourseOneStepDTO);
        }

        [HttpDelete]
        [Route("{courseId:int}")]
        public async Task<IActionResult> deleteCourse([FromRoute] int courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.UserCourses)
                .Include(c => c.CourseTags)
                .Include(c => c.CourseChapters)
                .ThenInclude(cc => cc.SubChapters)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
