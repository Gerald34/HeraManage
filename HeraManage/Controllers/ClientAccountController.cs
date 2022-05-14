using Microsoft.AspNetCore.Mvc;
using HeraManage.Requests;
using HeraManage.Services;

namespace HeraManage.Controllers
{
    [ApiController]
    [Route("/api/accounts")]
    public class ClientAccountController : ControllerBase
    {
        private ClientService _clientService;
        private ClientPointsService _clientPointsService;
        private dynamic _response = new { };
        public ClientAccountController(
            ClientService clientService,
            ClientPointsService clientPointsService)
        {
            _clientService = clientService;
            _clientPointsService = clientPointsService;
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

        [HttpGet("points/{uid}")]
        public async Task<IActionResult> GetClientPoints(Guid uid)
        {
            var data = await _clientPointsService.GetClientPoints(uid);
            return (data != null)
                ? Ok(new { error = false, message = "User points collection", points = data })
                : BadRequest(new { error = true, message = "No points collection" });
        }

        [HttpPut("points/{uid}/{purchaseAmount}")]
        public async Task<IActionResult> AddClientPoints(Guid uid, double purchaseAmount)
        {
            _response = await _clientPointsService.AddPoints(uid, purchaseAmount);
            return (!_response.error) ? Ok(_response) : BadRequest(_response);
        }

        [HttpPut("redeem/{uid}/{usedPoints}")]
        public async Task<IActionResult> RedeemPoints(Guid uid, int usedPoints)
        {
            _response = await _clientPointsService.RedeemClientPoints(uid, usedPoints);
            return (!_response.error) ? Ok(_response) : BadRequest(_response);
        }
    }
}