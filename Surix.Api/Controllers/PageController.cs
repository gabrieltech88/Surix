using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace Surix.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class PageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public PageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        private IActionResult GetFile(string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            return PhysicalFile(filePath, "text/html");
        }
        
        [HttpGet]
        public IActionResult Index() => GetFile("index.html");

        [HttpGet("surix")]
        [Authorize]
        public IActionResult GetSurix() => GetFile("surix.html");

        [HttpGet("cadastro")]
        public IActionResult GetCadastro() => GetFile("cadastro.html");

        [HttpGet("calculadora")]
        [Authorize]
        public IActionResult GetCalculadora() => GetFile("calculadora.html");

        [HttpGet("sures")]
        [Authorize]
        public IActionResult GetSures() => GetFile("sures.html");

    }
}
