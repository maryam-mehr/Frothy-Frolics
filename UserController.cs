using FrothyFrolics_server_.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FrothyFrolics_server_.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }


        

        [HttpGet]
        public IActionResult GetByEmail([FromQuery] string emailId)
        {
            
            var user = _userRepository.GetByEmail(emailId);

            return Json(user);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user.loyaltyPoints>500)
            {
                user.loyaltyPoints = 500;
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Update([FromBody] User user)
        {
            if (user.loyaltyPoints > 500)
            {
                user.loyaltyPoints = 500;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userRepository.Update(user);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult DeleteUserByEmail([FromBody] String email)
        {
            
            try
            {
                _userRepository.Delete(email);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

            return Ok(email);
        }
    }
}
