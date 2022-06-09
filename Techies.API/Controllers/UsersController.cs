using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Techies.Common.UsersDto;
using Techies.Data;
using Techies.Services.JwtWebAuthentication;
using Techies.Services.UsersRepository;

namespace Techies.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IUserRepository _userRepository;

        public UsersController(IJWTManagerRepository jWTManager, IUserRepository userRepository)
        {
            this._jWTManager = jWTManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<Users> GetAllUsers()
        {
            var data = _userRepository.GetAll().ToList();
            List<Users> finalData = data.Select(x => new Users()
            {
                Name = x.Name,
                //Password = x.Password
            }).ToList();
            return finalData;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users usersdata)
        {
            if (ModelState.IsValid)
            {
                usersdata.Password = HashPassword.EncodePasswordToBase64(usersdata.Password);
                var data = _userRepository.GetAll().Where(x => x.Name == usersdata.Name && x.Password == usersdata.Password).FirstOrDefault();
                if (data == null)
                {
                    return Unauthorized();
                }
                var token = _jWTManager.Authenticate(usersdata);
                return Ok(token);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegistrationDto users)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    users.Password = HashPassword.EncodePasswordToBase64(users.Password);
                    User userData = new User
                    {
                        Name = users.Email,
                        Password = users.Password
                    };
                    _userRepository.Add(userData);
                    
                    return Ok();
                }catch(Exception ex)
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
