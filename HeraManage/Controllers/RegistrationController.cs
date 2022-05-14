using Microsoft.AspNetCore.Mvc;
using HeraManage.Requests;
using HeraManage.Entities;
using HeraManage.Utils;
using HeraManage.Services;

namespace HeraManage.Controllers
{
    [ApiController]
    [Route("/api/registration")]
    public class RegistrationController : ControllerBase
    {
        private UserService _userService;
        public RegistrationController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationRequest registration)
        {

            if (registration.username == null)
            {
                var errorResponse = new { error = true, message = "Username cannot be empty" };
                return BadRequest(errorResponse);
            }

            if (registration.password == null)
            {
                var errorResponse = new { error = true, message = "Password cannot be empty" };
                return BadRequest(errorResponse);
            }

            int gender = 0;
            if (registration.gender > 0)
            {
                gender = registration.gender;
            }

            string hashedPassword = PasswordEncryptor.EncriptString(registration.password, 1000);

            UserEntity user = new UserEntity
            {
                username = registration.username,
                password = hashedPassword,
                firstName = registration.firstName,
                lastName = registration.lastName,
                dob = registration.dob,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                gender = gender,
                active = 0,
                verified = false
            };
            var response = _userService.CreateAccount(user);
            if (response.error) return BadRequest(response);
            return Ok(response);
        }
    }
}

