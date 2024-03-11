using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;
using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Both;
using PEP.Models.DTO.Courses.Presentation;
using PEP.Repositories.Implement;
using PEP.Repositories.Interface;


namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
 
        private readonly ICourseRepository impCourseRepository;
        private readonly IMapper mapper;

        public CoursesController( ICourseRepository impCourseRepository, IMapper mapper)
        {
 
            this.impCourseRepository = impCourseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesList([FromQuery] string? fitlerQuery=null, [FromQuery] int pageNumber=1, [FromQuery] int? pageSize=null)
        {
            var allCourses = await impCourseRepository.GetAllCoursesListAsync(fitlerQuery, pageNumber, pageSize);

            return Ok(mapper.Map<List<CoursesOverviewDTO>>(allCourses));
        }



        [HttpGet]
        [Route("CourseDesc")]
        public async Task<IActionResult> GetCourseDescById([FromQuery] int courseId)
        {
            var course = await impCourseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CourseDescDTO>(course));
        }
        [HttpGet]
        [Route("CourseSubChapters")]
        public async Task<IActionResult> GetCourseSubChapters([FromQuery] int subChapterId)
        {
            var courseSubChapter = await impCourseRepository.GetSubChapterById(subChapterId);
            if (courseSubChapter == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PreCoursesSubChapterWithMDDTO>(courseSubChapter));
        }


        [HttpPost]
        [Route("AddStepOne")]
        public async Task<IActionResult> AddCourseStepOne([FromBody] CoursesStepOneDTO addCourseStepOneDTO)
        {
            var courseDomainModel = mapper.Map<Course>(addCourseStepOneDTO);

            var exstingcourseDomainModel = await impCourseRepository.AddCourseAsync(courseDomainModel);

            return Ok(mapper.Map<CourseDescDTO>(exstingcourseDomainModel));
        }


        [HttpPost]
        [Route("AddStepTwo/{courseId:int}")]
        public async Task<IActionResult> AddCourseStepTwo([FromRoute] int courseId, [FromBody] CoursesStepTwoDTO coursesStepTwoDTO)
        {
            var courseDomainModel = mapper.Map<Course>(coursesStepTwoDTO);
            var exstingCourseDomainModel = await impCourseRepository.UpdateCourseStepTwoAsync(courseId, courseDomainModel);
            if (exstingCourseDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CourseDescDTO>(exstingCourseDomainModel));
        }
        [HttpPost]
        [Route("AddSubChapter")]
        public async Task<IActionResult> AddSubChapter(  [FromBody] AddCoursesSubChapterByChapterIdDTO addCoursesSubChapterDTO)
        {
            var subChapterDomainModel = mapper.Map<SubChapter>(addCoursesSubChapterDTO);
            var exstingCourseDomainModel = await impCourseRepository.AddSubChapter(subChapterDomainModel);
            if (exstingCourseDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PreCoursesSubChapterDTO>(exstingCourseDomainModel));
        }


        [HttpPut]
        [Route("UpdateStepOne/{courseId:int}")]
        public async Task<IActionResult> UpdateCourseOneStep([FromRoute] int courseId, [FromBody] CoursesStepOneDTO updateCourseOneStepDTO)
        {


            var courseDomainModel = mapper.Map<Course>(updateCourseOneStepDTO);
            var exstingCourseDomainModel = await impCourseRepository.UpdateCourseStepOneAsync(courseId, courseDomainModel);
            if (exstingCourseDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CourseDescDTO>(exstingCourseDomainModel));
        }

        [HttpPut]
        [Route("UpdateStepTwo/{courseId:int}")]
        public async Task<IActionResult> UpdateCourseTwoStep([FromRoute] int courseId, [FromBody] CoursesStepTwoDTO updateCourseOneStepDTO)
        {


            var courseDomainModel = mapper.Map<Course>(updateCourseOneStepDTO);
            var exstingCourseDomainModel = await impCourseRepository.UpdateCourseStepTwoAsync(courseId, courseDomainModel);
            if (exstingCourseDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CourseDescDTO>(exstingCourseDomainModel));
        }

        [HttpPut]
        [Route("UpdateSubChapter/{subchapterId:int}")]
        public async Task<IActionResult> UpdateSubChapter([FromRoute] int subchapterId, [FromBody] AddCoursesSubChapterDTO updateSubChapter)
        {

            var subChapter= mapper.Map<SubChapter>(updateSubChapter);
 
            var updatedSubChapter = await impCourseRepository.UpdateSubChapter(subchapterId, subChapter);
            if (updatedSubChapter == null)
                return NotFound();

            return Ok(mapper.Map<AddCoursesSubChapterDTO>(updatedSubChapter));
        }




        [HttpDelete]
        [Route("{courseId:int}")]
        public async Task<IActionResult> deleteCourseById([FromRoute] int courseId)
        {
            var course = await impCourseRepository.DeleteCourseByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CourseDescDTO>(course));
        }



        [HttpDelete]
        [Route("SubChapter/{subChapterId:int}")]
        public async Task<IActionResult> deleteSubChapterIdById([FromRoute] int subChapterId)
        {
            var subChapter = await impCourseRepository.DeleteSubChapterById(subChapterId);
            if (subChapter == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PreCoursesSubChapterDTO>(subChapter));
        }
    }
}
