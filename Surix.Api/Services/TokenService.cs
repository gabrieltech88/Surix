using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.VisualBasic;
using Surix.Api.Data.Models;

namespace Surix.Api.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", user.UserName),
            new Claim("id", user.Id)
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaSuperSeguraQuePrecisaTerNoMinimo32Caracteres"));
        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (expires: DateTime.Now.AddMinutes(30),
            claims: claims,
            signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}