using Microsoft.AspNetCore.Mvc;
using eBizTakeHomeApiChallenge.Services;

namespace eBizTakeHomeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly MainService _mainService;

        public MainController()
        {
            _mainService = new MainService();
        }

        // This endpoint consolidates all data into one JSON response
        [HttpGet("eBizdata")]
        public IActionResult GeteBizData()
        {
            var eBizData = _mainService.GeteBizData();
            return Ok(eBizData);
        }
    }
}
