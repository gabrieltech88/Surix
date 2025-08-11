using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DAL;
using Surix.Api.Data.DTO;
using System.ComponentModel.DataAnnotations;


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

        [HttpPost("sure")]
        public async Task<IActionResult> NewSure([FromBody] SureCreateRequest dto)
        {
            string result = await _sureDAL.CreateSure(dto);
            return Ok(result);
        }
    }
}