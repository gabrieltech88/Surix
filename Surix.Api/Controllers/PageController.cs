using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Surix.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class PageController : ControllerBase
    {
        private readonly string _pagesPath;

        public PageController(IWebHostEnvironment env)
        {
            // Caminho até a pasta Surix.Front
            _pagesPath = Path.Combine(env.ContentRootPath, "..", "Surix.Front");
        }

        // Página pública
        [HttpGet]
        public IActionResult Index()
        {
            var filePath = Path.Combine(_pagesPath, "index.html");
            return PhysicalFile(filePath, "text/html");
        }

        // Página protegida
        [HttpGet("surix")]
        [Authorize]
        public IActionResult GetSurix()
        {
            var filePath = Path.Combine(_pagesPath, "surix.html");
            return PhysicalFile(filePath, "text/html");
        }

        [HttpGet("cadastro")]
        public IActionResult GetCadastro()
        {
            var filePath = Path.Combine(_pagesPath, "cadastro.html");
            return PhysicalFile(filePath, "text/html");
        }


        [HttpGet("calculadora")]
        [Authorize]
        public IActionResult GetCalculadora()
        {
            var filePath = Path.Combine(_pagesPath, "calculadora.html");
            return PhysicalFile(filePath, "text/html");
        }

        [HttpGet("sures")]
        [Authorize]
        public IActionResult GetSures()
        {
            var filePath = Path.Combine(_pagesPath, "sures.html");
            return PhysicalFile(filePath, "text/html");
        }
    }
}
