﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEP.Data;
using PEP.Models;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses;
using PEP.Models.DTO.Courses.Add;
using PEP.Models.DTO.Courses.Presentation;
using PEP.Repositories.Implement;
using PEP.Repositories.Interface;


namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly FinalDesignContext dbContext;
        private readonly ICourseRepository impCourseRepository;
        private readonly IMapper mapper;

        public CoursesController(FinalDesignContext dbContext, ICourseRepository impCourseRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.impCourseRepository = impCourseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesList()
        {
            var allCourses = await impCourseRepository.GetAllCoursesListAsync();
            
            return Ok(mapper.Map<List<CoursesOverviewDTO>>(allCourses));
        }



        [HttpGet]
        [Route("{courseId:int}")]
        public async Task<IActionResult> GetCourseDescById([FromRoute] int courseId)
        {
            var course = await  impCourseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }
            mapper.Map<CoursesOverviewDTO>(course);
            return Ok(courseDTO);
        }
        [HttpPost]
        [Route("AddStepOne")]
        public async Task<IActionResult> AddCourseStepOne([FromBody] CoursesStepOneDTO addCourseStepOneDTO)
        {
            var courseDomainModel = mapper.Map<Course>(addCourseStepOneDTO);

            var exstingcourseDomainModel = await impCourseRepository.AddCourseAsync(courseDomainModel);

            var addCourseResultDTO = mapper.Map<CoursesStepOneDTO>(exstingcourseDomainModel);
            return Ok(addCourseResultDTO);
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
            var addCourseResultDTO = mapper.Map<CoursesStepTwoDTO>(exstingCourseDomainModel);
            return Ok(addCourseResultDTO);
        }

        [HttpPost]
        [Route("StepOne/{courseId:int}")]
        public async Task<IActionResult> UpdateCourseOneStep([FromRoute] int courseId, [FromBody] CoursesStepOneDTO updateCourseOneStepDTO)
        {


            var courseDomainModel = mapper.Map<Course>(updateCourseOneStepDTO);
            var exstingCourseDomainModel = await impCourseRepository.UpdateCourseStepOneAsync(courseId, courseDomainModel);

            return Ok(mapper.Map<CoursesStepOneDTO>(exstingCourseDomainModel));
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
