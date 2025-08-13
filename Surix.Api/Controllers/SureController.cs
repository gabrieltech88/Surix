using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DAL;
using Surix.Api.Data.DTO;
using Surix.Api.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace Surix.Api.Controllers
{
    [ApiController]
    [Route("sure")]
    public class SureController : ControllerBase
    {
        private readonly SureDAL _sureDAL;
        private readonly SureService _sureService;

        public SureController(SureDAL sureDAL, SureService sureService)
        {
            _sureDAL = sureDAL;
            _sureService = sureService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewSure([FromBody] SureCreateRequest dto)
        {
            var id = User.FindFirstValue("id");

            int result = await _sureDAL.CreateSure(dto, id);
            return Ok(result);
        }

        [HttpGet("content")]
        [Authorize]
        public async Task<IActionResult> Sures([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var id = User.FindFirstValue("id");

            var result = await _sureDAL.GetSures(id, pageNumber, pageSize);

            return Ok(result);
        }


        [HttpGet("roi")]
        [Authorize]
        public async Task<IActionResult> Roi()
        {
            var id = User.FindFirstValue("id");

            var result = await _sureDAL.GetRoi(id);

            return Ok(result);
        }

        [HttpGet("stats")]
        [Authorize]
        public async Task<IActionResult> Stats()
        {
            var id = User.FindFirstValue("id");

            var result = await _sureDAL.GetStats(id);

            return Ok(result);
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> InfosSure()
        {

            var result = await _sureService.GetInfoSures();

            return Ok(result);
        }

    }
}