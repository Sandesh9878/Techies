using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Techies.Data;
using Techies.Services.CURDRepository;
using Techies.Services.JwtWebAuthentication;

namespace Techies.API.Controllers
{
    [Authorize]
    [Route("api/CURD")]
    [ApiController]
    public class CURDController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly ICURDRepository _userRepository;
        public CURDController(IJWTManagerRepository jWTManager, ICURDRepository userRepository)
        {
            _jWTManager = jWTManager;
            _userRepository = userRepository;
        }
        [HttpGet]
        public List<Student> GetLists()
        {
            var data = _userRepository.GetAll().ToList();
            List<Student> finalData = data.Select(x => new Student()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Email = x.Email,
                PhoneNo = x.PhoneNo,
                FatherName=x.FatherName,
                MotherName=x.MotherName
            }).ToList();
            return finalData;
        }
        [HttpGet("GetStudentById/{id}")]
        public Student GetStudentById(int id)
        {
            var Student = _userRepository.GetById(id);
            return Student;
        }
        [HttpPost]
        public IActionResult Create([FromBody] Student Student)
        {
            bool isSuccess = false;
            try
            {
                _userRepository.Add(Student);
                isSuccess = true;
            }
            catch (System.Exception ex)
            {

                isSuccess = false;
            }

            return Ok(Student);
        }
        [HttpPut]
        public IActionResult Update([FromBody]Student Student)
        {
            _userRepository.update(Student);
            return Ok(Student);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Student = _userRepository.GetById(id);
            if (Student == null)
            {
                return NotFound();
            }

            _userRepository.Remove(Student);

            return Ok(Student);
        }
    }
}
