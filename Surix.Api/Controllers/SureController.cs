using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DAL;
using Surix.Api.Data.DTO;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace Surix.Api.Controllers
{
    [ApiController]
    [Route("sure")]
    public class SureController : ControllerBase
    {
        private readonly SureDAL _sureDAL;

        public SureController(SureDAL sureDAL)
        {
            _sureDAL = sureDAL;
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
        public async Task<IActionResult> Sures([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 4)
        {
            var id = User.FindFirstValue("id");

            var result = await _sureDAL.GetSures(id, pageNumber, pageSize);

            return Ok(result);
        }
    }
}