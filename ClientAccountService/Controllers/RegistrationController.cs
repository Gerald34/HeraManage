using Microsoft.AspNetCore.Mvc;
using ClientAccountService.Requests;
using ClientAccountService.Utils;
using ClientAccountService.Services;

namespace ClientAccountService.Controllers
{
    [ApiController]
    [Route("/api/registration")]
    public class RegistrationController : ControllerBase
    {
        private ClientService _clientService;
        public RegistrationController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("register")]
        public IActionResult Register(ClientAccountRequest clientAccountRequest)
        {

            if (clientAccountRequest.Email == null)
            {
                var errorResponse = new { error = true, message = "Username cannot be empty" };
                return BadRequest(errorResponse);
            }

            if (clientAccountRequest.Password == null)
            {
                var errorResponse = new { error = true, message = "Password cannot be empty" };
                return BadRequest(errorResponse);
            }

            int gender = 0;
            if (clientAccountRequest.Gender > 0)
            {
                gender = clientAccountRequest.Gender;
            }

            string hashedPassword = PasswordEncryptor.EncriptString(clientAccountRequest.Password, 1000);

            var response = _clientService.CreateClientAccount(clientAccountRequest);
            if (response.error) return BadRequest(response);
            return Ok(response);
        }
    }
}

