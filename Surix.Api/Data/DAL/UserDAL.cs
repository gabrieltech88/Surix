using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Surix.Api.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Surix.Api.Data.DTO;
using Microsoft.AspNetCore.Components.Web;

namespace Surix.Api.Data.DAL
{
    [ApiController]
    [Route("user")]
    public class UserDAL : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;

        public UserDAL(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest dto)
        {
            try
            {
                User user = _mapper.Map<User>(dto);
                IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    Thread.Sleep(5000);
                    return Ok("Usuário cadastrado com sucesso!");
                }
                else
                {
                    foreach (var erro in result.Errors)
                    {
                        Console.WriteLine($"Erro: {erro.Code} - {erro.Description}");
                    }
                    return BadRequest("Falha ao cadastrar usuário");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Menssagem de erro:{ex.Message}");
                Console.WriteLine($"Pilha de erro: {ex.StackTrace}");
                Console.WriteLine($"Origem do erro: {ex.Source}");

                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPatch("password")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetRequest dto)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(dto.UserName);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, dto.Password);

                if (result.Succeeded)
                {
                    return Ok("Senha trocada com sucesso!");
                }
                else
                {
                    foreach (var erro in result.Errors)
                    {
                        Console.WriteLine($"Erro: {erro.Code} - {erro.Description}");
                    }
                    return BadRequest("Falha ao cadastrar usuário");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Menssagem de erro:{ex.Message}");
                Console.WriteLine($"Pilha de erro: {ex.StackTrace}");
                Console.WriteLine($"Origem do erro: {ex.Source}");

                return StatusCode(500, "Erro interno no servidor");
            }
            
        }
    }
}