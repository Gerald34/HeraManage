using Microsoft.AspNetCore.Mvc;
using ClientAccountPointsService.Services;

namespace ClientAccountPointsService.Controllers
{

    [ApiController]
    class ClientAccountPointsController : ControllerBase
    {
        private dynamic _response = new { };
        private ClientAccPointsService _clientAccPointsService;

        [HttpGet("points/{uid}")]
        public async Task<IActionResult> GetClientPoints(Guid uid)
        {
            var data = await _clientAccPointsService.GetClientPoints(uid);
            return (data != null)
                ? Ok(new { error = false, message = "User points collection", points = data })
                : BadRequest(new { error = true, message = "No points collection" });
        }

        [HttpPut("points/{uid}/{purchaseAmount}")]
        public async Task<IActionResult> AddClientPoints(Guid uid, double purchaseAmount)
        {
            _response = await _clientAccPointsService.AddPoints(uid, purchaseAmount);
            return (!_response.error) ? Ok(_response) : BadRequest(_response);
        }

        [HttpPut("redeem/{uid}/{usedPoints}")]
        public async Task<IActionResult> RedeemPoints(Guid uid, int usedPoints)
        {
            _response = await _clientAccPointsService.RedeemClientPoints(uid, usedPoints);
            return (!_response.error) ? Ok(_response) : BadRequest(_response);
        }
    }
}