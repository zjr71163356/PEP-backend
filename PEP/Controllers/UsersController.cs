using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEP.Models.Domain;
using PEP.Models.DTO.Courses.Presentation;
using PEP.Models.DTO.User;
using PEP.Repositories.Implement;
using PEP.Repositories.Interface;
using System.Data;

namespace PEP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository impUserRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository impUserRepository, IMapper mapper)
        {
            this.impUserRepository = impUserRepository;
            this.mapper = mapper;
        }


        [HttpPost]
        [Route("AddUserCourse")]
        public async Task<IActionResult> AddUserCourse([FromBody] UserCourseAddDTO userCourseAddDTO)
        {
            var userCourse = mapper.Map<UserCourse>(userCourseAddDTO);
            var result = await impUserRepository.AddUserCourseToMyList(userCourse);

            if (result == null)
            {
                return BadRequest("课程重复");
            }

            return Ok(mapper.Map<UserCourseAddDTO>(result));


        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterDTO userRegisterDTO)
        {
            var isTaken = await impUserRepository.IsUsernameTakenAsync(userRegisterDTO.UserName);

            if (isTaken)
            {
                return BadRequest("用户名重复");
            }
            var user = mapper.Map<User>(userRegisterDTO);
            return Ok(await impUserRepository.RegisterUserAsync(user));


        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDTO userLoginDTO)
        {
            var user = mapper.Map<User>(userLoginDTO);
            var loginUser = await impUserRepository.LoginUserAsync(user);
            var loginUserDTO = mapper.Map<UserLoginResultDTO>(loginUser);
            if (loginUserDTO == null) { return BadRequest(loginUserDTO); }

            return Ok(loginUserDTO);

        }

        [HttpGet]
        [Route("isUserNameRepeat")]
        public async Task<IActionResult> isUserNameRepeat([FromQuery] string userName)
        {
            var isUserNameRepeat = await impUserRepository.IsUsernameTakenAsync(userName);


            return Ok(new { userName, isUserNameRepeat });

        }
        [HttpGet]
        [Route("isUserAccountRepeat")]
        public async Task<IActionResult> isUserAccountRepeat([FromQuery] string userAccount)
        {
            var isUserAccountRepeat = await impUserRepository.IsUserAccountTakenAsync(userAccount);


            return Ok(new { userAccount, isUserAccountRepeat });

        }

        [HttpGet]
        [Route("GetUserCourseList/{userId:int}")]
        public async Task<IActionResult> GetUserCoursesList([FromRoute] int userId)
        {
            var userCourseList = await impUserRepository.GetUserCoursesListAsync(userId);
            if (userCourseList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<CoursesOverviewDTO>>(userCourseList));

        }

        [HttpGet]
        [Route("isUserCourseRepeat")]
        public async Task<IActionResult> isUserCourseRepeat([FromQuery] int userId, [FromQuery] int courseId)
        {
            var result = await impUserRepository.IsUserCourseRepeat(userId, courseId);
            return Ok(result);

        }

        [HttpDelete]
        [Route("RemoveCourseFromMyList")]
        public async Task<IActionResult> RemoveCourseFromMyList([FromQuery] int userId, [FromQuery] int courseId)
        {
            var result = await impUserRepository.RemoveCourseFromMyList(userId, courseId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserCourseAddDTO>(result));

        }
    }
}
