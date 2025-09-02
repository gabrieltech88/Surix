using AutoMapper;
using Microsoft.AspNetCore.Identity;

using Surix.Api.Data.DTO;
using Surix.Api.Data.Models;

namespace Surix.Api.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;


    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<string> Login(Data.DTO.LoginRequest dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao logar usuário");
        }

        User user = _signInManager
            .UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

        var token = _tokenService.GenerateToken(user);

        return token;
    }

    
}