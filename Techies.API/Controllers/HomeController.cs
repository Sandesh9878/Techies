using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Techies.Common.Home;
using Techies.Data;
using Techies.Services.HomeRepository;
using Techies.Services.JwtWebAuthentication;
using Techies.Services.UsersRepository;

namespace Techies.API.Controllers
{
    [Authorize]
    [Route("api/Home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IHomeRepository _userRepository;
        public HomeController(IJWTManagerRepository jWTManager, IHomeRepository userRepository)
        {
            _jWTManager = jWTManager;
            _userRepository = userRepository;
        }

        [HttpGet]
        public List<staff> GetLists()
        {
            var data = _userRepository.GetAll().ToList();
            List<staff> finalData = data.Select(x => new staff()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Email = x.Email,
                Mobile = x.Mobile
            }).ToList();
            return finalData;
        }

        [HttpGet("GetStaffById/{id}")]
        public staff GetStaffById(int id)
        {
            var staff = _userRepository.GetById(id);
            return staff;
        }

        [HttpPost]
        public IActionResult Create([FromBody]staff staff)
        {
            bool isSuccess = false;
            try
            {
                _userRepository.Add(staff);
                isSuccess = true;
            }
            catch (System.Exception ex)
            {

                isSuccess = false;
            }
            
            return Ok(staff);
        }
        [HttpPut]
        public IActionResult updateStaff([FromBody]staff staff)
        {
            _userRepository.update(staff);
            return Ok(staff);    
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public  IActionResult DeleteStaff(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staff =  _userRepository.GetById(id);
            if (staff == null)
            {
                return NotFound();
            }

            _userRepository.Remove(staff);

            return Ok(staff);
        }

        
    }


   
}
