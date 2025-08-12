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
        public IActionResult SurixPage()
        {
            var filePath = Path.Combine(_pagesPath, "surix.html");
            return PhysicalFile(filePath, "text/html");
        }

        // Página genérica (rota = nome do arquivo)
        [HttpGet("cadastro")]
        public IActionResult GetCadastro()
        {
            var filePath = Path.Combine(_pagesPath, "cadastro.html");
            return PhysicalFile(filePath, "text/html");
        }
    }
}
