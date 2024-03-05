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
        public IActionResult GetAllCoursesList()
        {
            var allcourses = dbContext.Courses.Include(c => c.CourseTags).ToList();
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
        public IActionResult GetCourseById([FromRoute] int courseId)
        {
            var course = dbContext.Courses
                .Include(c => c.CourseTags)
                .Include(c => c.CourseChapters)
                .ThenInclude(cc => cc.SubChapters)
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }
            var courseDTO = new CourseGetByIdDTO
            {
                CourseId=course.CourseId,
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
        public IActionResult AddCourse([FromBody] CoursesAddDTO addCourseDTO)
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
                        CourseId = cc.CourseId,
                        Title = sc.Title,
                        ParentChapterId = cc.ChapterId,
                        SubChapterNumber = sc.SubChapterNumber,
                        ParentChapterNumber = cc.ChapterNumber,
                        MarkdownContent = sc.MarkdownContent
                    }).ToList()
                }).ToList()

            };



            dbContext.Courses.Add(courseDomainModel);
            dbContext.SaveChanges();

            return Ok(addCourseDTO);
        }

        [HttpPut]
        [Route("{courseId:int}")]
        public IActionResult UpdateCourseOneStep([FromRoute] int courseId, [FromBody] CoursesUpdateOneStepDTO updateCourseOneStepDTO)
        {
            var course = dbContext.Courses
                .Include(c => c.CourseTags)
                .FirstOrDefault(c => c.CourseId == courseId);
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
            foreach (var ct in updateCourseOneStepDTO.CourseTags)
            {
                ct.TagColor = new Random().Next(1, 6);
            }
            course.CourseTags = updateCourseOneStepDTO.CourseTags.Select(ct => new CourseTag
            {
                TagName = ct.TagName,
                TagColor = ct.TagColor
            }).ToList();

           

            dbContext.SaveChanges();
            return Ok(updateCourseOneStepDTO);
        }

        [HttpDelete]
        [Route("{courseId:int}")]
        public IActionResult deleteCourse([FromRoute] int courseId)
        {
            var course = dbContext.Courses
                .Include(c => c.UserCourses)
                .Include(c => c.CourseTags)
                .Include(c => c.CourseChapters)
                .ThenInclude(cc => cc.SubChapters)
                .FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }
            dbContext.Courses.Remove(course);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
