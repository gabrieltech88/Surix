using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DTO;
using Surix.Api.Services;
using System.ComponentModel.DataAnnotations;


namespace Surix.Api.Controllers
{
    [ApiController]
    [Route("user/manipulation")]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var token = await _userService.Login(dto);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = false,         // protege o token contra JS (recomendado)
                Secure = true,           // só enviar em HTTPS
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            return Ok("Usuário Logado");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok("Usuário Deslogado");
        }
    }
}

