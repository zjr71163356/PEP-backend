using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEP.Model.Domain;
using PEP.Models.DTO;

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
            var allcourses = dbContext.Courses.ToList();
            var allcoursesDTO = new List<CoursesDTO>();
            foreach (var course in allcourses)
            {
                allcoursesDTO.Add(new CoursesDTO
                {
                    CourseName = course.CourseName,
                    ChapterCount = course.ChapterCount,
                    Introduction = course.Introduction,
                    ImageUrl = course.ImageUrl,
                    CourseTags = course.CourseTags
                });
            }
            return Ok(allcoursesDTO);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetUserCoursesList(int userId)
        {
 
            var userCoursesList = dbContext.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => new CoursesDTO
                {
                    CourseName = uc.Course.CourseName,
                    ChapterCount = uc.Course.ChapterCount,
                    Introduction = uc.Course.Introduction,
                    ImageUrl = uc.Course.ImageUrl,
                    CourseTags = uc.Course.CourseTags
                })
                .ToList();

            return Ok(userCoursesList);
        }
    }
}
