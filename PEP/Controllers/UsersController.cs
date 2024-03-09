﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEP.Models.Domain;
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
            var loginUserDTO = mapper.Map<UserLoginDTO>(loginUser);
            if (loginUserDTO == null) { return BadRequest(new UserLoginDTO { }); }

            return Ok(new UserLoginDTO { Role=loginUserDTO.Role});

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
    }
}