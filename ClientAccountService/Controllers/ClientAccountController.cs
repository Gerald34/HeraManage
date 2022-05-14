using Microsoft.AspNetCore.Mvc;
using ClientAccountService.Requests;
using ClientAccountService.Services;

namespace ClientAccountService.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class ClientAccountController : ControllerBase
    {
        private ClientService _clientService;
        private dynamic _response = new { };
        public ClientAccountController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("get-account-details")]
        public async Task<IActionResult> GetAccountDetails()
        {

            return await Ok(_response);
        }

        [HttpPost("create")]
        public Task<IActionResult> CreateAccount(ClientAccountRequest clientAccountRequest)
        {
            var process = _clientService.CreateClientAccount(clientAccountRequest);
            return (process.error) ? BadRequest(process) : Ok(process);
        }

    }
}