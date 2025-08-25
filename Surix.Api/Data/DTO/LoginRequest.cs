using System.ComponentModel.DataAnnotations;

namespace Surix.Api.Data.DTO;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}