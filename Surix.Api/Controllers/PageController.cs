using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Surix.Api.Controllers
{
    [ApiController]
    [Route("popopo")]
    public class PageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public PageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetIndex()
        {
            // Caminho absoluto do projeto
            var indexPath = Path.Combine(_env.ContentRootPath, "..", "..", "Surix.Front", "index.html");

            // Debug opcional
            Console.WriteLine("Caminho do index.html: " + indexPath);

            if (!System.IO.File.Exists(indexPath))
                return NotFound("Arquivo index.html não encontrado no caminho: " + indexPath);

            var htmlContent = await System.IO.File.ReadAllTextAsync(indexPath);
            return Content(htmlContent, "text/html");
        }
    }
}
