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
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserAddDTO userAddDTO)
        {
            var user = mapper.Map<User>(userAddDTO);
            var result= await impUserRepository.AddUser(user);
            return Ok(mapper.Map<UserPreDTO>(result));

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
        [Route("AddUserSubmission")]
        public async Task<IActionResult> AddUserSubmission([FromBody] UserSubmissionAddDTO userSubmissionAddDTO)
        {
            var submissionRecord = mapper.Map<SubmissionRecord>(userSubmissionAddDTO);
            var result = await impUserRepository.AddSubmissionRecord(submissionRecord);

            if (result == null)
            {
                return BadRequest("添加失败");
            }

            return Ok(mapper.Map<UserSubmissionAddDTO>(result));


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
        [Route("isUserCourseRepeat")]
        public async Task<IActionResult> isUserCourseRepeat([FromQuery] int userId, [FromQuery] int courseId)
        {
            var result = await impUserRepository.IsUserCourseRepeat(userId, courseId);
            return Ok(result);

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
        [Route("GetUserSubmission")]
        public async Task<IActionResult> GetUserSubmission([FromQuery] int userId, [FromQuery] int? problemId, [FromQuery] int pageNumber = 1, [FromQuery] int? pageSize = null)
        {
            var result = await impUserRepository.GetSubmissionRecords(userId, problemId, pageNumber, pageSize);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<UserSubmissionPreDTO>>(result));

        }

        [HttpGet]
        [Route("GetSubmissionRecordById/{recordId:int}")]
        public async Task<IActionResult> GetSubmissionRecordById([FromRoute] int recordId)
        {
            var result = await impUserRepository.GetSubmissionRecordById(recordId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserSubmissionPreDTO>(result));

        }

        [HttpGet]
        [Route("GetUserById/{userId:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            var result = await impUserRepository.GetUserById(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserPreDTO>(result));

        }

        [HttpGet]
        [Route("GetUserList")]
        public async Task<IActionResult> GetUserList(int pageNumber = 1, int? pageSize = null)
        {
            var result = await impUserRepository.GetUserList(pageNumber, pageSize);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<UserPreDTO>>(result));

        }

        [HttpPut]
        [Route("UpdateUser/{userId:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserAddDTO userUpdateDTO)
        {
            var user = mapper.Map<User>(userUpdateDTO);
            var result = await impUserRepository.UpdateUserById(userId, user);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserPreDTO>(result));

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

        [HttpDelete]
        [Route("RemoveUser")]
        public async Task<IActionResult> RemoveUser([FromQuery] int userId)
        {
            var result = await impUserRepository.RemoveUser(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserPreDTO>(result));

        }

    }
}
