using Microsoft.AspNetCore.Mvc;
using HeraManage.Requests;
using HeraManage.Repositories;
using Microsoft.AspNetCore.Authorization;
using HeraManage.Entities;

namespace HeraManage.Controllers
{
    [ApiController]
    [Route("/api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private IUserRepository _userRepository;

        public AuthenticationController(
            IUserRepository userRepository,
            ILogger<AuthenticationController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult post(AuthenticateRequest loginRequest)
        {
            var response = _userRepository.Authenticate(loginRequest);
            if (response.error)
            {
                return BadRequest(new
                {
                    error = true,
                    message = "Username or password is incorrect."
                });
            }

            if (response.data.active == 0)
            {
                return NotFound(new { error = true, message = "Account not activated, please verify account to continue." });
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all admin users
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpGet]
        public async Task<IActionResult> GetAllAdminUsers()
        {
            var users = _userRepository.GetAll();
            if (users == null)
            {
                var errorResponse = new
                {
                    error = true,
                    message = "No users found"
                };

                return BadRequest(errorResponse);
            }

            dynamic response = new
            {
                error = false,
                message = "User collection found.",
                totalCount = users.Count(),
                data = users
            };

            return await Ok(response);
        }

        /// <summary>
        /// Verify account
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("verification")]
        public dynamic VerifyAccount(int userID, string username)
        {
            return _userRepository.ActivateAccount(userID, username);
        }

    }
}
