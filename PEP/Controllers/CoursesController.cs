using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models;
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
        [Route("User/{userId:int}")]
        public IActionResult GetUserCoursesList([FromRoute] int userId)
        {

            var userCoursesList = dbContext.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => new CoursesOverviewDTO
                {
                    CourseName = uc.Course.CourseName,
                    ChapterCount = uc.Course.ChapterCount,
                    Introduction = uc.Course.Introduction,
                    ImageUrl = uc.Course.ImageUrl,
                    CourseTags = uc.Course.CourseTags.Select(ct => new CoursesTagDTO
                    {
                        TagName = ct.TagName,
                        TagColor = ct.TagColor
                    }).ToList()
                })
                .ToList();
            if (userCoursesList.Count == 0)
            {
                return NotFound();
            }
            return Ok(userCoursesList);
        }
        // ...

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
                CourseTags=addCourseDTO.CourseTags.Select(ct=>new CourseTag
                {
                    TagName=ct.TagName,
                    TagColor=ct.TagColor
                }).ToList(),
                CourseChapters = addCourseDTO.CourseChapters.Select(cc => new CourseChapter
                {
                    Title = cc.Title,
                    ChapterNumber = cc.ChapterNumber,
                    SubChapters = cc.SubChapters.Select(sc => new SubChapter
                    {
                        CourseId = cc.CourseId,
                        Title = sc.Title,
                        ParentChapterId=cc.ChapterId,
                        SubChapterNumber = sc.SubChapterNumber,
                        ParentChapterNumber=cc.ChapterNumber,
                        MarkdownContent = sc.MarkdownContent
                    }).ToList()
                }).ToList()
            
            };

           

            dbContext.Courses.Add(courseDomainModel);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetAllCoursesList), null);
        }
    }
}
