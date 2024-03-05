using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models.DTO.Courses;

namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly FinalDesignContext dbContext;

        public UserCoursesController(FinalDesignContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetUserCoursesList([FromRoute] int userId)
        {
            var userCoursesList = await dbContext.UserCourses
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Course)
                .ThenInclude(c => c.CourseTags)
                .ToListAsync();

            if (userCoursesList.Count == 0)
            {
                return NotFound();
            }

            var coursesOverviewDTOList = userCoursesList.Select(uc => new CoursesOverviewDTO
            {
                CourseId = uc.CourseId,
                CourseName = uc.Course.CourseName,
                ChapterCount = uc.Course.ChapterCount,
                Introduction = uc.Course.Introduction,
                ImageUrl = uc.Course.ImageUrl,
                CourseTags = uc.Course.CourseTags.Select(ct => new CoursesTagDTO
                {
                    TagName = ct.TagName,
                    TagColor = ct.TagColor
                }).ToList()
            }).ToList();

            return Ok(coursesOverviewDTOList);
        }
    }
}
